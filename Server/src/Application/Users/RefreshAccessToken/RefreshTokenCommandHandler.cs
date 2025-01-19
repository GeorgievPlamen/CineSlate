using Application.Common;
using Application.Users.Interfaces;
using Application.Users.Login;

using Domain.Common;
using Domain.Users.Errors;

using MediatR;

namespace Application.Users.RefreshAccessToken;

public class RefreshTokenCommandHandler(IUserIdentity userIdentity, IUserRepository userRepository) : IRequestHandler<RefreshTokenCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var currentRefreshToken = await userRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);
        if (currentRefreshToken is null)
            return Result<LoginResponse>.Failure(UserErrors.TokenEpired());

        if (currentRefreshToken.ExpiresAt < DateTime.UtcNow)
            return Result<LoginResponse>.Failure(UserErrors.TokenEpired());

        if (currentRefreshToken.UserId is null)
            return Result<LoginResponse>.Failure(Error.ServerError());

        var user = await userRepository.GetByIdAsync(currentRefreshToken.UserId, cancellationToken);
        if (user is null)
            return Result<LoginResponse>.Failure(UserErrors.NotFound());

        var token = userIdentity.GenerateRefreshToken();
        var refreshToken = RefreshToken.Create(Guid.NewGuid(), user.Id, token);
        if (!await userRepository.CreateRefreshTokenAsync(refreshToken, cancellationToken))
            return Result<LoginResponse>.Failure(Error.ServerError());

        var jwtToken = userIdentity.GenerateJwtToken(user.Id, user.Username, user.Email, user.Role.ToString());

        return Result<LoginResponse>.Success(new LoginResponse(
            user.Username.Value,
            user.Email,
            jwtToken,
            token,
            user.Id.Value,
            user.Bio));
    }
}

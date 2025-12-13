using MediatR;
using Application.Users.Interfaces;
using Domain.Users.Errors;
using Application.Common;
using Domain.Common;

namespace Application.Users.Login;

public class LoginQueryHandler(IUserRepository usersRepository, IUserIdentity userIdentity) :
    IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var foundUser = await usersRepository.GetAsync(request.Email, cancellationToken);
        if (foundUser is null)
            return Result<LoginResponse>.Failure(UserErrors.NotFound(request.Email));

        bool hasValidPassword = userIdentity.ValidatePassword(request.Password, foundUser.PasswordHash);
        if (!hasValidPassword)
            return Result<LoginResponse>.Failure(UserErrors.NotFound(request.Email));

        var rToken = userIdentity.GenerateRefreshToken();
        var refreshToken = RefreshToken.Create(Guid.NewGuid(), foundUser.Id, rToken);
        if (!await usersRepository.CreateRefreshTokenAsync(refreshToken, cancellationToken))
            return Result<LoginResponse>.Failure(Error.ServerError());

        var token = userIdentity.GenerateJwtToken(
            foundUser.Id,
            foundUser.Username,
            foundUser.Email,
            foundUser.Role.ToString());

        LoginResponse result = new(
            foundUser.Username.Value,
            foundUser.Email,
            token,
            rToken,
            foundUser.Id.Value,
            foundUser.Bio ?? "",
            foundUser.AvatarImageBase64 ?? "");

        return Result<LoginResponse>.Success(result);
    }
}

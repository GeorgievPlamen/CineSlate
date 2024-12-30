using MediatR;
using Application.Users.Interfaces;
using Domain.Users.Errors;
using Application.Common;

namespace Application.Users.Login;

public class LoginQueryHandler(IUserRepository usersRepository, IUserIdentity userIdentity) :
    IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _usersRepository = usersRepository;
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var foundUser = await _usersRepository.GetAsync(request.Email, cancellationToken);

        if (foundUser is null)
            return Result<LoginResponse>.Failure(UserErrors.NotFound(request.Email));

        bool hasValidPassword = _userIdentity.ValidatePassword(request.Password, foundUser.PasswordHash);

        if (!hasValidPassword)
            return Result<LoginResponse>.Failure(UserErrors.NotFound(request.Email));

        var token = _userIdentity.GenerateJwtToken(
            foundUser.Id,
            foundUser.Username,
            foundUser.Email,
            foundUser.Role.ToString());

        LoginResponse result = new(
            foundUser.Username.Value,
            foundUser.Email,
            token,
            foundUser.Id.Value);

        return Result<LoginResponse>.Success(result);
    }
}

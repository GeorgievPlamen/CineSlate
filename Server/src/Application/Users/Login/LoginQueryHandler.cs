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
        var users = await _usersRepository.GetUsersAsync(cancellationToken);

        var foundUser = users.Find(
            u => u.Email == request.Email);

        if (foundUser is null)
        {
            return Result<LoginResponse>.Failure(UserErrors.UserNotFound);
        }

        bool hasValidPassword = _userIdentity.ValidatePassword(request.Password, foundUser.PasswordHash);

        if (!hasValidPassword)
        {
            return Result<LoginResponse>.Failure(UserErrors.UserNotFound);
        }

        var token = _userIdentity.GenerateJwtToken(
            foundUser.Id.Value,
            foundUser.Name.First,
            foundUser.Name.Last,
            foundUser.Email,
            foundUser.Role.ToString());

        LoginResponse result = new(
            foundUser.Name.First,
            foundUser.Name.Last,
            foundUser.Email,
            token);

        return Result<LoginResponse>.Success(result);
    }
}

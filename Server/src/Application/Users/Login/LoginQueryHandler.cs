using MediatR;
using Application.Users.Interfaces;
using Domain.Users.Errors;
using Application.Common;

namespace Application.Users.Login;

public class LoginQueryHandler(IUsersRepository usersRepository, IUserIdentity userIdentity) :
    IRequestHandler<LoginQuery, Result<LoginResponse>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetUsersAsync();

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
            foundUser.Id,
            foundUser.Name.First,
            foundUser.Name.Last,
            foundUser.Email,
            foundUser.Role);

        LoginResponse result = new(
            foundUser.Id,
            foundUser.Name.First,
            foundUser.Name.Last,
            foundUser.Email,
            token);

        return Result<LoginResponse>.Success(result);
    }
}

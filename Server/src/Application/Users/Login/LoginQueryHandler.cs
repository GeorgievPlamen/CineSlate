using MediatR;
using Application.Users.Interfaces;
using Domain.Users.Errors;
using Application.Common;

namespace Application.Users.Login;

public class LoginQueryHandler(IUsersRepository usersRepository, IUserIdentity userIdentity) :
    IRequestHandler<LoginQuery, Result<AuthResponse>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IUserIdentity _userIdentity = userIdentity;

    public async Task<Result<AuthResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetUsersAsync();

        var foundUser = users.Find(
            u => u.Email == request.Email);

        if (foundUser is null)
        {
            return Result<AuthResponse>.Failure(UserErrors.UserNotFound);
        }

        var token = _userIdentity.GenerateJwtToken(
            foundUser.Id,
            foundUser.FirstName,
            foundUser.LastName,
            foundUser.Role);

        AuthResponse result = new(
            foundUser.Id,
            foundUser.FirstName,
            foundUser.LastName,
            foundUser.Email,
            token);

        return Result<AuthResponse>.Success(result);
    }
}

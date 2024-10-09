using Application.Users.Interfaces;
using MediatR;
using Domain.Users;
using Application.Common;
using Domain.Users.Errors;
using Domain.Common;

namespace Application.Users.Register;

public class RegisterCommandHandler(
    IUserIdentity userIdentity,
    IUsersRepository userRepository) : IRequestHandler<RegisterCommand, Result<Unit>>
{
    private readonly IUserIdentity _userIdentity = userIdentity;
    private readonly IUsersRepository _userRepository = userRepository;

    public async Task<Result<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserAsync(request.Email, cancellationToken);

        if (existingUser is not null)
        {
            return Result<Unit>.Failure(UserErrors.AlreadyRegistered);
        }

        string passwordHash = _userIdentity.HashPassword(request.Password);

        User user = User.CreateUser(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash);

        bool isSuccess = await _userRepository.AddUserAsync(user, cancellationToken);

        return isSuccess ?
        Result<Unit>.Success(Unit.Value) :
        Result<Unit>.Failure(Error.ServerError());
    }
}
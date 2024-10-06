using Application.Users.Interfaces;
using MediatR;
using Domain.Users;
using Application.Common;
using Domain.Users.Errors;

namespace Application.Users.Register;

public class RegisterCommandHandler(
    IUserIdentity userIdentity,
    IUsersRepository userRepository) : IRequestHandler<RegisterCommand, Result<Unit>>
{
    private readonly IUserIdentity _userIdentity = userIdentity;
    private readonly IUsersRepository _userRepository = userRepository;

    public async Task<Result<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        string passwordHash = _userIdentity.HashPassword(request.Password);

        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
        };

        bool hasRegistered = await _userRepository.AddUserAsync(user);

        return hasRegistered ?
        Result<Unit>.Success(Unit.Value) :
        Result<Unit>.Failure(UserErrors.AlreadyRegistered);
    }
}
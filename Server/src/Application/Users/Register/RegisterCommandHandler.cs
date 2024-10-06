using Application.Users.Interfaces;
using MediatR;
using Domain.Users;
using Application.Common;

namespace Application.Users.Register;

public class RegisterCommandHandler(
    IUsersRepository userRepository) : IRequestHandler<RegisterCommand, Result<Unit>>
{
    private readonly IUsersRepository _userRepository = userRepository;

    public async Task<Result<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
        };

        await _userRepository.AddUserAsync(user);

        return Result<Unit>.Success(Unit.Value);
    }
}
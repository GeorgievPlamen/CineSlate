using Application.Users.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Users;

namespace Application.Users.Register;

public class RegisterCommandHandler(IUsersRepository userRepository, ILogger<RegisterCommandHandler> logger) : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUsersRepository _userRepository = userRepository;
    private readonly ILogger<RegisterCommandHandler> _logger = logger;
    public Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetUsers();

        _logger.LogInformation(users.ToString());

        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
        };

        AuthResponse result = new(user.Id, user.FirstName, user.LastName, user.Email, "authtoken");

        return Task.FromResult(result);
    }
}
using Application.User.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.User.Register;

public class RegisterCommandHandler(IUserRepository userRepository, ILogger<RegisterCommandHandler> logger) : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<RegisterCommandHandler> _logger = logger;
    public Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetUsers();

        _logger.LogInformation(users.ToString());

        Domain.User.User user = new()
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
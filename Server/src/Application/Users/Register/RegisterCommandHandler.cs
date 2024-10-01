using Application.Users.Interfaces;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Users;

namespace Application.Users.Register;

public class RegisterCommandHandler(IUsersRepository userRepository, ILogger<RegisterCommandHandler> logger, IJwtGenerator jwtGenerator) : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUsersRepository _userRepository = userRepository;
    private readonly ILogger<RegisterCommandHandler> _logger = logger;
    private readonly IJwtGenerator _jwtGenerator = jwtGenerator;

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

        string jwt = _jwtGenerator.GetToken(user.Id, user.FirstName, user.LastName);

        AuthResponse result = new(user.Id, user.FirstName, user.LastName, user.Email, jwt);

        return Task.FromResult(result);
    }
}
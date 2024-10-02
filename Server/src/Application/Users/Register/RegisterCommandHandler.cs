using Application.Users.Interfaces;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Users;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Users.Register;

public class RegisterCommandHandler(
    IUsersRepository userRepository,
    ILogger<RegisterCommandHandler> logger,
    IJwtGenerator jwtGenerator,
    IValidator<RegisterCommand> validator) : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUsersRepository _userRepository = userRepository;
    private readonly ILogger<RegisterCommandHandler> _logger = logger;
    private readonly IJwtGenerator _jwtGenerator = jwtGenerator;
    private readonly IValidator<RegisterCommand> _validator = validator;

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetUsers();

        ValidationResult validationResult = await _validator.ValidateAsync(request);

        _logger.LogInformation(validationResult.ToString());
        System.Console.WriteLine(validationResult);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.ToString());
        }

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

        return result;
    }
}
using MediatR;
using Application.Common.Interfaces;
using Application.Users.Interfaces;
using Domain.Users.Errors;
using Microsoft.Extensions.Logging;
using Application.Common;

namespace Application.Users.Login;

public class LoginQueryHandler(IUsersRepository usersRepository, IJwtGenerator jwtGenerator, ILogger<LoginQueryHandler> logger) :
    IRequestHandler<LoginQuery, Result<AuthResponse>>
{
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IJwtGenerator _jwtGenerator = jwtGenerator;
    private readonly ILogger<LoginQueryHandler> _logger = logger;

    public async Task<Result<AuthResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var users = await _usersRepository.GetUsersAsync();

        foreach (var user in users)
        {
            _logger.LogInformation($"{user.FirstName} {user.LastName} {user.Email}");
        }

        var foundUser = users.Find(
            u => u.Email == request.Email &&
            u.Password == request.Password);

        if (foundUser is null)
        {
            return Result<AuthResponse>.Failure(UserErrors.UserNotFound);
        }

        var token = _jwtGenerator.GetToken(
            foundUser.Id,
            foundUser.FirstName,
            foundUser.LastName);

        AuthResponse result = new(
            foundUser.Id,
            foundUser.FirstName,
            foundUser.LastName,
            foundUser.Email,
            token);

        return Result<AuthResponse>.Success(result);
    }
}

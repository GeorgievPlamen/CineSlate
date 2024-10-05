using Application.Users.Interfaces;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Users;
using Domain.Common;

namespace Application.Users.Register;

public class RegisterCommandHandler(
    IUsersRepository userRepository,
    IJwtGenerator jwtGenerator) : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    private readonly IUsersRepository _userRepository = userRepository;
    private readonly IJwtGenerator _jwtGenerator = jwtGenerator;

    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
        };

        await _userRepository.AddUserAsync(user);

        string jwt = _jwtGenerator.GetToken(user.Id, user.FirstName, user.LastName);

        AuthResponse authResponse = new(user.Id, user.FirstName, user.LastName, user.Email, jwt);
        Result<AuthResponse> result = Result<AuthResponse>.Success(authResponse);

        return result;
    }
}
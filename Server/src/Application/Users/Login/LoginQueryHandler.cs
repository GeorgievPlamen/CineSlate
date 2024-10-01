using MediatR;
using Domain.Users;

namespace Application.Users.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
{
    public Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        User user = new()
        {
            Email = request.Email,
            Password = request.Password
        };

        AuthResponse result = new(user.Id, "John", "Doe", user.Email, "authtoken");

        return Task.FromResult(result);
    }
}

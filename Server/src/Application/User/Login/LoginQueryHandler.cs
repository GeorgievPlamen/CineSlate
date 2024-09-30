using MediatR;

namespace Application.User.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthResponse>
{
    public Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        Domain.User.User user = new()
        {
            Email = request.Email,
            Password = request.Password
        };

        AuthResponse result = new(user.Id, "John", "Doe", user.Email, "authtoken");

        return Task.FromResult(result);
    }
}

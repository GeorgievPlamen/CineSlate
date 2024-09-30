using MediatR;

namespace Application.User.Login;

public record LoginQuery(string Email, string Password) : IRequest<AuthResponse>;
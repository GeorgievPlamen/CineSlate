using MediatR;

namespace Application.Users.Login;

public record LoginQuery(string Email, string Password) : IRequest<AuthResponse>;
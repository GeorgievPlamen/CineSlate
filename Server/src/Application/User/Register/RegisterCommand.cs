using MediatR;

namespace Application.User.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<AuthResponse>;

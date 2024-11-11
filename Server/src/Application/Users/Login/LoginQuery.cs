using Application.Common;
using MediatR;

namespace Application.Users.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;
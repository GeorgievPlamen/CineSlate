using Application.Common;
using Domain.Users.ValueObjects;
using MediatR;

namespace Application.Users.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Result<UserId>>;

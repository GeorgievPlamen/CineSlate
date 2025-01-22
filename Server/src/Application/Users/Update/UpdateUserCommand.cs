using Application.Common;
using Application.Users.Me;

using Domain.Users.ValueObjects;

using MediatR;

namespace Application.Users.Update;

public record UpdateUserCommand(UserId Id, string? Bio, string? Picture) : IRequest<Result<MeResponse>>;
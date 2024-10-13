using Application.Common;
using MediatR;

namespace Application.Users.Me;

public record MeQuery : IRequest<Result<MeResponse>>;
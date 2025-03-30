using Application.Common;

using MediatR;

namespace Application.Watchlist.UpdateWatchedStatus;

public record UpdateWatchedStatusCommand(int MovieId, bool Watched) : IRequest<Result<Unit>>;
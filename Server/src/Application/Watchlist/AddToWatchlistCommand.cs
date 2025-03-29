using Application.Common;

using MediatR;

namespace Application.Watchlist;

public record AddToWatchlistCommand(int MovieId) : IRequest<Result<Unit>>;
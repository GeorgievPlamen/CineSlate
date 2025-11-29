using Application.Common;

using MediatR;

namespace Application.Watchlist.AddToWatchlist;

public record AddToWatchlistCommand(int MovieId) : IRequest<Result<Unit>>;
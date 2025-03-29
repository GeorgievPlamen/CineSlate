using Application.Common;

using MediatR;

namespace Application.Watchlist.RemoveFromWatchlist;

public record RemoveFromWatchlistCommand(int MovieId, bool Watched) : IRequest<Result<Unit>>;
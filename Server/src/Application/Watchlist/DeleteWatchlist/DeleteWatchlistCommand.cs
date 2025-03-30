using Application.Common;

using MediatR;

namespace Application.Watchlist.DeleteWatchlist;

public record DeleteWatchlistCommand : IRequest<Result<Unit>>;
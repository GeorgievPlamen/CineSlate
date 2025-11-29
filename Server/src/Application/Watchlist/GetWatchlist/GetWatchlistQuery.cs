using Application.Common;

using MediatR;

namespace Application.Watchlist.GetWatchlist;

public record GetWatchlistQuery : IRequest<Result<GetWatchlistResponse>>;
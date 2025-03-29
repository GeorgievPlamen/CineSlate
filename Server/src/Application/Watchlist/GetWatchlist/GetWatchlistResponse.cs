using Domain.Watchlist.ValueObjects;

namespace Application.Watchlist.GetWatchlist;

public record GetWatchlistResponse(List<KeyValuePair<int, bool>> Watchlist);
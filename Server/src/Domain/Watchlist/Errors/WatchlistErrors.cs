using Domain.Common;
using Domain.Watchlist.ValueObjects;

namespace Domain.Watchlist.Errors;

public static class WatchlistErrors
{
    public static Error NotFound(WatchlistId id) => Error.NotFound("Watchlist.NotFound", $"Watchlist with id: '{id.Value}', not found.");
}
using Domain.Common;
using Domain.Watchlist.ValueObjects;

namespace Domain.Watchlist.Errors;

public static class WatchlistErrors
{
    public static Error NotFound(WatchlistId id) => Error.NotFound("Watchlist.NotFound", $"Watchlist with id: '{id.Value}', not found.");
    public static Error NotFound() => Error.NotFound("Watchlist.NotFound", $"Watchlist for this user is not found.");
}
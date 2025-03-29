using Domain.Watchlist;
using Domain.Watchlist.ValueObjects;

namespace Application.Watchlist.Interfaces;

public interface IWatchlistRepository
{
    Task<WatchlistAggregate?> GetWatchlistAsync(WatchlistId id, CancellationToken cancellationToken);
    Task<bool> UpdateWatchlistAsync(WatchlistAggregate watchlist, CancellationToken cancellationToken);
    Task<bool> CreateWatchlistAsync(WatchlistAggregate watchlist, CancellationToken cancellationToken);
    Task<bool> DeleteWatchlistAsync(WatchlistId id, CancellationToken cancellationToken);
}
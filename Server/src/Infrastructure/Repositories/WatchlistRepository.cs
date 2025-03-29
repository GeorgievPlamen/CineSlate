using Application.Watchlist.Interfaces;

using Domain.Watchlist;
using Domain.Watchlist.ValueObjects;

using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class WatchlistRepository(CineSlateContext dbContext) : IWatchlistRepository
{
    public async Task<bool> CreateWatchlistAsync(WatchlistAggregate watchlist, CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteWatchlistAsync(WatchlistId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<WatchlistAggregate?> GetWatchlistAsync(WatchlistId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateWatchlistAsync(WatchlistAggregate watchlist, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

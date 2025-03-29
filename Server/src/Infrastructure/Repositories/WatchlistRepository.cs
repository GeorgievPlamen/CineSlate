using Application.Watchlist.Interfaces;

using Domain.Users.ValueObjects;
using Domain.Watchlist;
using Domain.Watchlist.ValueObjects;

using Infrastructure.Database;
using Infrastructure.Repositories.MappingExtensions;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WatchlistRepository(CineSlateContext dbContext) : IWatchlistRepository
{
    public async Task<bool> CreateWatchlistAsync(WatchlistAggregate watchlist, CancellationToken cancellationToken)
    {
        dbContext.Add(watchlist.ToModel());

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteWatchlistAsync(WatchlistId id, CancellationToken cancellationToken)
    {
        var affectedRows = await dbContext.Watchlists
            .Where(x => x.Id == id.Value)
            .ExecuteDeleteAsync(cancellationToken);

        return affectedRows > 0;
    }

    public async Task<WatchlistAggregate?> GetWatchlistAsync(WatchlistId id, CancellationToken cancellationToken)
    {
        var watchlist = await dbContext.Watchlists
            .Include(x => x.MovieToWatchModels)
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        return watchlist?.Unwrap();
    }

    public async Task<WatchlistAggregate?> GetWatchlistByUserIdAsync(UserId id, CancellationToken cancellationToken)
    {
        var watchlist = await dbContext.Watchlists
            .Include(x => x.MovieToWatchModels)
            .FirstOrDefaultAsync(x => x.UserId == id.Value, cancellationToken);

        return watchlist?.Unwrap();
    }

    public async Task<bool> UpdateWatchlistAsync(WatchlistAggregate watchlist, CancellationToken cancellationToken)
    {
        var newWatchlist = watchlist.ToModel();

        var affectedRows = await dbContext.Watchlists
            .Where(x => x.Id == newWatchlist.Id)
            .ExecuteUpdateAsync(
                x => x.SetProperty(p => p.MovieToWatchModels, newWatchlist.MovieToWatchModels),
                cancellationToken);

        return affectedRows > 0;
    }
}

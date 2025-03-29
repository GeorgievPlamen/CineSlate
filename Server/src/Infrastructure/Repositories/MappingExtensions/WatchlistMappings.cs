using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;
using Domain.Watchlist;
using Domain.Watchlist.ValueObjects;
using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class WatchlistMappings
{
    public static List<MovieToWatchModel> ToModel(this IEnumerable<MovieToWatch> movieToWatch)
        => [.. movieToWatch.Select(x => new MovieToWatchModel
        {
            MovieId = x.MovieId.Value,
            Watched = x.Watched
        })];

    public static WatchlistModel ToModel(this WatchlistAggregate watchlist) => new()
    {
        Id = watchlist.Id.Value,
        MovieToWatchModels = watchlist.Movies.ToModel(),
        UserId = watchlist.User.Value,
    };

    public static WatchlistAggregate Unwrap(this WatchlistModel model) => WatchlistAggregate.Create
    (
        UserId.Create(model.Id),
        [.. model.MovieToWatchModels.Select(x => MovieToWatch.Create(MovieId.Create(x.MovieId), x.Watched))]
    );
}
using Domain.Common.Models;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;
using Domain.Watchlist.Exceptions;
using Domain.Watchlist.ValueObjects;

namespace Domain.Watchlist;

public class WatchlistAggregate : AggregateRoot<WatchlistId>
{
    private WatchlistAggregate(WatchlistId id) : base(id) { }
    private List<MovieToWatch> _movies = [];

    public UserId User { get; private set; } = null!;
    public IReadOnlyList<MovieToWatch> Movies => [.. _movies];

    public static WatchlistAggregate Create(UserId userId) => new(WatchlistId.Create()) { User = userId };
    public static WatchlistAggregate Create(UserId userId, List<MovieToWatch> movies) => new(WatchlistId.Create())
    {
        User = userId,
        _movies = movies,
    };

    public void AddMovie(MovieId movieId) => _movies.Add(MovieToWatch.Create(movieId));
    public void RemoveMovie(MovieId movieId, bool hasWatched) => _movies.Remove(MovieToWatch.Create(movieId, hasWatched));

    public void SetWatched(MovieId movieId)
    {
        var movie = MovieToWatch.Create(movieId);

        var notFound = _movies.Remove(movie);

        if (notFound) throw new MovieNotFoundInWatchlistException(movieId);

        _movies.Add(MovieToWatch.Create(movieId, true));
    }
}

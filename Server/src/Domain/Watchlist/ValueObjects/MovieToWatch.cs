using Domain.Common.Models;
using Domain.Movies.ValueObjects;

namespace Domain.Watchlist.ValueObjects;

public class MovieToWatch : ValueObject
{
    public MovieId MovieId { get; private set; } = null!;
    public bool Watched { get; private set; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return MovieId;
        yield return Watched;
    }

    public static MovieToWatch Create(MovieId movieId, bool watched = false)
        => new() { MovieId = movieId, Watched = watched };
}
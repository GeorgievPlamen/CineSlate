using Domain.Common.Models;
using Domain.Movies.ValueObjects;

namespace Domain.Movies;

public class MovieAggregate : AggregateRoot<MovieId>
{
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateOnly ReleaseDate { get; private set; }
    public string PosterPath { get; private set; } = null!;
    private List<Genre> _genres { get; set; } = null!;
    public IReadOnlyList<Genre> Genres => [.. _genres];

    private MovieAggregate(MovieId id) : base(id) { }

    public static MovieAggregate Create(
        MovieId id,
        string title,
        string description,
        DateOnly releaseDate,
        string posterPath,
        IEnumerable<Genre> genres
        ) => new(id)
        {
            Title = title,
            Description = description,
            ReleaseDate = releaseDate,
            PosterPath = posterPath,
            _genres = genres.ToList()
        };
};
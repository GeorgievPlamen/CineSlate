using Domain.Common.Models;
using Domain.Movies.ValueObjects;

namespace Domain.Movies;

public class MovieAggregate : AggregateRoot<MovieId>
{
    private MovieAggregate(MovieId id) : base(id) { }

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateOnly ReleaseDate { get; private set; }
    public string PosterPath { get; private set; } = null!;
    private List<Genre> _genres { get; set; } = null!;
    public IReadOnlyList<Genre> Genres => [.. _genres];
    private List<Rating> _ratings { get; set; } = null!;
    public IReadOnlyCollection<Rating> Ratings => [.. _ratings];
    public double Rating => _ratings.Average(r => r.Value);

    public static MovieAggregate Create(
        MovieId id,
        string title,
        string description,
        DateOnly releaseDate,
        string posterPath,
        IEnumerable<Genre> genres) => new(id)
        {
            Title = title,
            Description = description,
            ReleaseDate = releaseDate,
            PosterPath = "https://image.tmdb.org/t/p/w500/" + posterPath,
            _genres = genres.ToList()
        };

    public void AddRating(Rating rating) => _ratings.Add(rating);
};
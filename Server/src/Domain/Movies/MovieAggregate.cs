using Domain.Common.Models;
using Domain.Movies.Ratings;
using Domain.Movies.ValueObjects;

namespace Domain.Movies;

public class MovieAggregate(MovieId id) : AggregateRoot<MovieId>(id)
{
    private List<Genre> _genres = [];
    private readonly List<Rating> _ratings = [];

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateOnly ReleaseDate { get; private set; }
    public string PosterPath { get; private set; } = null!;
    public IReadOnlyList<Genre> Genres => [.. _genres];
    public IReadOnlyCollection<Rating> Ratings => [.. _ratings];
    public double Rating => Ratings.Count > 0 ? Ratings.Average(r => r.Value) : 0;
    public MovieDetails Details { get; private set; } = null!;

    public static MovieAggregate Create(
        MovieId id,
        string title,
        string description,
        DateOnly releaseDate,
        string posterPath,
        IEnumerable<Genre> genres,
        MovieDetails? details = null) => new(id)
        {
            Title = title,
            Description = description ?? string.Empty,
            ReleaseDate = releaseDate,
            PosterPath = posterPath ?? string.Empty,
            _genres = [.. genres],
            Details = details ?? MovieDetails.CreateEmpty()
        };

    public void AddDetails(MovieDetails movieDetails, IEnumerable<Genre> genres)
    {
        Details = movieDetails;
        _genres = [.. genres];
    }

    public void AddRating(Rating rating) => _ratings.Add(rating);
};
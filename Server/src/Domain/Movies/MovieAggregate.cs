using Domain.Common.Models;
using Domain.Movies.Reviews;
using Domain.Movies.ValueObjects;

namespace Domain.Movies;

public class MovieAggregate(MovieId id) : AggregateRoot<MovieId>(id)
{
    private List<Genre> _genres = [];
    private readonly List<Review> _reviews = [];

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateOnly ReleaseDate { get; private set; }
    public string PosterPath { get; private set; } = null!;
    public IReadOnlyList<Genre> Genres => [.. _genres];
    public IReadOnlyCollection<Review> Reviews => [.. _reviews];
    public double Rating => Reviews.Count > 0 ? Reviews.Average(r => r.Rating) : 0;
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

    public void AddReview(Review rating) => _reviews.Add(rating);
};
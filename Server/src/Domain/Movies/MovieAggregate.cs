using Domain.Common.Models;
using Domain.Movies.Reviews;
using Domain.Movies.ValueObjects;

namespace Domain.Movies;

public class MovieAggregate(MovieId id) : AggregateRoot<MovieId>(id)
{
    private List<Genre> _genres = [];
    private List<Review> _reviews = [];
    private double _ratingBackup;

    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateOnly ReleaseDate { get; private set; }
    public string PosterPath { get; private set; } = null!;
    public IReadOnlyList<Genre> Genres => [.. _genres];
    public IReadOnlyCollection<Review> Reviews => [.. _reviews];
    public double Rating => Reviews.Count > 0 ? Reviews.Average(r => r.Rating) : _ratingBackup;
    public MovieDetails Details { get; private set; } = null!;

    public static MovieAggregate Create(
        MovieId id,
        string title,
        string description,
        DateOnly releaseDate,
        string posterPath,
        IEnumerable<Genre> genres,
        MovieDetails? details = null,
        double rating = 0) => new(id)
        {
            Title = title,
            Description = description ?? string.Empty,
            ReleaseDate = releaseDate,
            PosterPath = posterPath ?? string.Empty,
            _genres = [.. genres],
            Details = details ?? MovieDetails.CreateEmpty(),
            _reviews = [],
            _ratingBackup = rating
        };

    public void AddDetails(MovieDetails movieDetails, IEnumerable<Genre> genres)
    {
        Details = movieDetails;
        _genres = [.. genres];
    }

    public void AddReview(Review review) => _reviews.Add(review);
    public void AddReviews(IEnumerable<Review> reviews) => _reviews.AddRange(reviews);
};
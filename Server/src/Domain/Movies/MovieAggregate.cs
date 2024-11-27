using Domain.Common.Models;
using Domain.Movies.ValueObjects;

namespace Domain.Movies;

public class MovieAggregate : AggregateRoot<MovieId>
{
    private MovieAggregate(MovieId id) : base(id) { }
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
        IEnumerable<Genre> genres) => new(id)
        {
            Title = title,
            Description = description ?? string.Empty,
            ReleaseDate = releaseDate,
            PosterPath = posterPath ?? string.Empty,
            _genres = [.. genres],
            Details = MovieDetails.CreateEmpty()
        };

    public void AddDetails(MovieDetails movieDetails) => Details = movieDetails;
    public void AddRating(Rating rating) => _ratings.Add(rating);
};
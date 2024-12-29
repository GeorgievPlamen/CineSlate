using Domain.Movies.Reviews;

namespace Application.Reviews;

public record ReviewResponse(
    Guid Id,
    int Rating,
    string Text,
    int MovieId,
    Guid AuthorId,
    string AuthorUsername,
    bool ContainsSpoilers);

public record ReviewWithMovieDetailsResponse(
    string Title,
    int MovieId,
    DateOnly ReleaseDate,
    string PosterPath,
    ReviewResponse ReviewResponse);

public static class Converter
{
    public static ReviewResponse ToResponse(this Review review, string authorUsername) => new(
        review.Id.Value,
        review.Rating,
        review.Text,
        review.MovieId.Value,
        review.Author.Value,
        authorUsername,
        review.ContainsSpoilers);
}
using Domain.Movies.Reviews;

namespace Application.Reviews;

public record ReviewResponse(
    int Rating,
    string Text,
    int MovieId,
    Guid AuthorId,
    string AuthorUsername,
    bool ContainsSpoilers);

public static class Converter
{
    public static ReviewResponse ToResponse(this Review review, string authorUsername) => new(
        review.Rating,
        review.Text,
        review.MovieId.Value,
        review.Author.Value,
        authorUsername,
        review.ContainsSpoilers);
}
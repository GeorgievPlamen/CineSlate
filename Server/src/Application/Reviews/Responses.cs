using Domain.Movies.Reviews;
using Domain.Users.ValueObjects;

namespace Application.Reviews;

public record ReviewResponse(
    Guid Id,
    int Rating,
    string Text,
    int MovieId,
    Guid AuthorId,
    string AuthorUsername,
    bool ContainsSpoilers,
    int Likes,
    bool HasUserLiked);

public record ReviewDetailsResponse(
    Guid Id,
    int Rating,
    string Text,
    int MovieId,
    Guid AuthorId,
    bool ContainsSpoilers,
    int Likes,
    bool HasUserLiked,
    List<Username> UsersWhoLiked);

public record ReviewWithMovieDetailsResponse(
    string Title,
    int MovieId,
    DateOnly ReleaseDate,
    string PosterPath,
    ReviewResponse ReviewResponse);

public static class Converter
{
    public static ReviewResponse ToResponse(
        this Review review,
        string authorUsername,
        bool hasUserLiked) => new(
            review.Id.Value,
            review.Rating,
            review.Text,
            review.MovieId.Value,
            review.Author.Value,
            authorUsername,
            review.ContainsSpoilers,
            review.LikesCount,
            hasUserLiked);

    public static ReviewDetailsResponse ToDetailsResponse(
        this Review review,
        bool hasUserLiked,
        List<Username> usersWhoLiked) => new(
            review.Id.Value,
            review.Rating,
            review.Text,
            review.MovieId.Value,
            review.Author.Value,
            review.ContainsSpoilers,
            review.LikesCount,
            hasUserLiked,
            usersWhoLiked);
}
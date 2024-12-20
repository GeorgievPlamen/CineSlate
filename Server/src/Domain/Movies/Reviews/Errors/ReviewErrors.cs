using Domain.Common;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Movies.ValueObjects;

namespace Domain.Movies.Reviews.Errors;

public static class ReviewErrors
{
    public static Error UserAlreadyReviewed(string userId) => Error.BadRequest("Review.UserAlreadyReviewed", $"User with ID: {userId} has already reviewed this movie!");
    public static Error NotByUser(string userId) => Error.BadRequest("Review.NotByUser", $"User with ID: {userId} has not made this review!");
    public static Error NotFound(MovieId id) => Error.NotFound("Review.NotFound", $"Review for movie with id: '{id.Value}' is not found.");
    public static Error NotFound(ReviewId id) => Error.NotFound("Review.NotFound", $"Review with id: '{id.Value}' is not found.");
}
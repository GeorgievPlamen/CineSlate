using Domain.Common;

namespace Domain.Movies.Reviews.Errors;

public static class ReviewErrors
{
    public static Error UserAlreadyReviewed(string userId) => Error.BadRequest("Review.UserAlreadyReviewed", $"User with ID: {userId} has already reviewd this movie!");
}
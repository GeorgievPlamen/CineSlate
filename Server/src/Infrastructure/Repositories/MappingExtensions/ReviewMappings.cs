using Domain.Movies.Reviews;
using Domain.Users.ValueObjects;
using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class ReviewMappings
{
    public static ReviewModel ToModel(this Review review)
        => new()
        {
            Id = review.Id.Value,
            AuthorId = review.Author.Value,
            ContainsSpoilers = review.ContainsSpoilers,
            Rating = review.Rating,
            Text = review.Text
        };

    public static Review Unwrap(this ReviewModel model)
        => Review.Create(
            model.Rating,
            UserId.Create(model.AuthorId),
            model.Text,
            model.ContainsSpoilers);
}
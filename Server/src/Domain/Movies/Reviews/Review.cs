using Domain.Common.Models;
using Domain.Movies.Reviews.Exceptions;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Users.ValueObjects;

namespace Domain.Movies.Reviews;

public class Review(ReviewId id) : Entity<ReviewId>(id)
{
    public int Rating { get; private set; }
    public UserId Author { get; private set; } = null!;
    public string Text { get; private set; } = null!;
    public bool ContainsSpoilers { get; private set; }
    public static Review Create(int rating, UserId ratedBy, string text = "", bool containsSpoilers = false)
    {
        if (rating < 1 || rating > 5)
            throw new RatingOutOfRangeException();

        return new(ReviewId.Create())
        {
            Rating = rating,
            Author = ratedBy,
            Text = text,
            ContainsSpoilers = containsSpoilers,
        };
    }
}

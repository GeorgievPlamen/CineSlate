using Domain.Common.Models;
using Domain.Movies.Ratings.Exceptions;
using Domain.Movies.Ratings.ValueObjects;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;

namespace Domain.Movies.Ratings;

public class Rating(RatingId id) : Entity<RatingId>(id)
{
    public int Value { get; private set; }
    public MovieId MovieId { get; private set; } = null!;
    public UserId Author { get; private set; } = null!;
    public string Text { get; private set; } = null!;
    public bool ContainsSpoilers { get; private set; }
    public static Rating Create(int rating, MovieId movieId, UserId ratedBy, string text = "", bool containsSpoilers = false)
    {
        if (rating < 1 || rating > 5)
            throw new RatingOutOfRangeException();

        return new(RatingId.Create())
        {
            Value = rating,
            MovieId = movieId,
            Author = ratedBy,
            Text = text,
            ContainsSpoilers = containsSpoilers,
        };
    }
}

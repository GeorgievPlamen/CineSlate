using Domain.Common.Models;
using Domain.Movies.Reviews.Exceptions;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;

namespace Domain.Movies.Reviews;

public class Review(ReviewId id) : Entity<ReviewId>(id)
{
    private readonly List<Like> _likes = [];
    private readonly List<Comment> _comments = [];
    public int Rating { get; private set; }
    public MovieId MovieId { get; set; } = null!;
    public UserId Author { get; private set; } = null!;
    public string Text { get; private set; } = null!;
    public bool ContainsSpoilers { get; private set; }
    public IReadOnlyList<Like> Likes => [.. _likes];
    public IReadOnlyList<Comment> Comments => [.. _comments];
    public int LikesCount => _likes.Count;

    public void AddLikes(List<Like> likes) => _likes.AddRange(likes);
    public void RemoveLike(Like likes) => _likes.Remove(likes);
    public bool HasUserLiked(UserId userId) => _likes.FirstOrDefault(x => x.FromUserId == userId) is not null;

    public void AddComment(List<Comment> comments) => _comments.AddRange(comments);
    public void RemoveComment(Comment comment) => _comments.Remove(comment);
    public bool HasUserCommented(UserId userId) => _comments.FirstOrDefault(x => x.FromUserId == userId) is not null;

    public static Review Create(int rating, UserId ratedBy, string text = "", bool containsSpoilers = false)
    {
        return rating < 1 || rating > 5
            ? throw new RatingOutOfRangeException()
            : new(ReviewId.Create())
            {
                Rating = rating,
                Author = ratedBy,
                Text = text,
                ContainsSpoilers = containsSpoilers,
            };
    }

    public static Review Create(int rating, UserId ratedBy, MovieId movieId, string text = "", bool containsSpoilers = false)
    {
        return rating < 1 || rating > 5
            ? throw new RatingOutOfRangeException()
            : new(ReviewId.Create())
            {
                Rating = rating,
                Author = ratedBy,
                MovieId = movieId,
                Text = text,
                ContainsSpoilers = containsSpoilers,
            };
    }

    public static Review Create(Guid reviewId, int rating, UserId ratedBy, MovieId movieId, string text = "", bool containsSpoilers = false)
    {
        return rating < 1 || rating > 5
            ? throw new RatingOutOfRangeException()
            : new(ReviewId.Create(reviewId))
            {
                Rating = rating,
                Author = ratedBy,
                MovieId = movieId,
                Text = text,
                ContainsSpoilers = containsSpoilers,
            };
    }
}

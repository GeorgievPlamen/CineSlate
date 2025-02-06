using Domain.Movies.Reviews.ValueObjects;
using Domain.Users.ValueObjects;

using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class CommentsMapping
{
    public static CommentModel ToModel(this Comment comment, ReviewModel review) => new()
    {
        Review = review,
        UserId = comment.FromUserId.Value,
        Username = comment.FromUser,
        Comment = comment.Value,
    };

    public static Comment Unwrap(this CommentModel model) => Comment.Create(
        UserId.Create(model.UserId),
        model.Username,
        model.Comment);

    public static List<Comment> Unwrap(this List<CommentModel> models)
    {
        var result = new List<Comment>();

        foreach (var model in models)
        {
            result.Add(Comment.Create(UserId.Create(model.UserId), model.Username, model.Comment));
        }

        return result;
    }

    public static List<CommentModel> Unwrap(this List<Comment> comments, ReviewModel review)
    {
        var result = new List<CommentModel>();

        foreach (var comment in comments)
        {
            result.Add(comment.ToModel(review));
        }

        return result;
    }
}
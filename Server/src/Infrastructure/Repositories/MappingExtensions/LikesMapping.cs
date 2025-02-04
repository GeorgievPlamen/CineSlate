using Domain.Movies.Reviews.ValueObjects;
using Domain.Users.ValueObjects;

using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class LikesMapping
{
    public static LikesModel ToModel(this Like like, ReviewModel review) => new()
    {
        Review = review,
        UserId = like.FromUserId.Value,
        Username = like.FromUser
    };

    public static Like Unwrap(this LikesModel model) => Like.Create(UserId.Create(model.UserId), model.Username);

    public static List<Like> Unwrap(this List<LikesModel> models)
    {
        var result = new List<Like>();

        foreach (var model in models)
        {
            result.Add(Like.Create(UserId.Create(model.UserId), model.Username));
        }

        return result;
    }

    public static List<LikesModel> Unwrap(this List<Like> likes, ReviewModel review)
    {
        var result = new List<LikesModel>();

        foreach (var like in likes)
        {
            result.Add(like.ToModel(review));
        }

        return result;
    }
}
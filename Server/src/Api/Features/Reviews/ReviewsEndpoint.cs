
using Api.Common;
using Api.Features.Reviews.Requests.cs;
using Application.Reviews.Create;
using Domain.Movies.Reviews.ValueObjects;
using MediatR;

namespace Api.Features.Reviews;

public static class ReviewsEndpoint
{
    public const string Uri = "api/reviews";
    public const string Latest = "/";
    public const string Create = "/";
    public const string Update = "/";
    public static string Delete(Guid id) => $"/{id}";
    public static void MapReviews(this WebApplication app)
    {
        var reviews = app.MapGroup(Uri).RequireAuthorization();

        reviews.MapGet(Latest, () => TypedResults.Ok("latest reviews")); // TODO
        reviews.MapPost(Create, CreateReviewAsync); // TODO
        reviews.MapPut(Update, () => TypedResults.Ok("update")); // TODO
        reviews.MapPut("/{id}", (Guid id) => TypedResults.Ok($"update {id}")); // TODO
    }

    private static async Task<IResult> CreateReviewAsync(
        CreateReviewRequest request,
        ISender mediatr,
        CancellationToken cancellationToken)
            => Response<ReviewId>.Match(await mediatr.Send(new CreateReviewCommand(
                request.Rating,
                request.MovieId,
                request.Text,
                request.ContainsSpoilers
            ), cancellationToken));
}
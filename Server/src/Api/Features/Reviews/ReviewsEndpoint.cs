
using Api.Common;
using Api.Features.Reviews.Requests.cs;
using Application.Common;
using Application.Reviews;
using Application.Reviews.Create;
using Application.Reviews.Get;
using Domain.Movies.Reviews.ValueObjects;
using MediatR;

namespace Api.Features.Reviews;

public static class ReviewsEndpoint
{
    public const string Uri = "api/reviews";
    public const string Get = "/";
    public const string Create = "/";
    public const string Update = "/";
    public static string GetById(Guid id) => $"/{id}";
    public static string Delete(Guid id) => $"/{id}";
    public static void MapReviews(this WebApplication app)
    {
        var reviews = app.MapGroup(Uri).RequireAuthorization();

        reviews.MapGet(Get, GetReviewsAsync);
        reviews.MapPost(Create, CreateReviewAsync).WithName("Created");
        reviews.MapPut(Update, () => TypedResults.Ok("update")); // TODO
        reviews.MapDelete("/{id}", (Guid id) => TypedResults.Ok($"delete {id}")); // TODO
    }

    private static async Task<IResult> GetReviewsAsync(int page, ReviewsBy? reviewsBy, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<ReviewResponse>>.Match(await mediatr.Send(new GetReviewsQuery(page, reviewsBy ?? ReviewsBy.Latest), cancellationToken));

    private static async Task<IResult> CreateReviewAsync(
        CreateReviewRequest request,
        ISender mediatr,
        CancellationToken cancellationToken)
            => Response<ReviewId>.Match(await mediatr.Send(new CreateReviewCommand(
                request.Rating,
                request.MovieId,
                request.Text,
                request.ContainsSpoilers
            ), cancellationToken), "Created");
}
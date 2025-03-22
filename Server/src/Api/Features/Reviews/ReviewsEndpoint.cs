using Api.Common;
using Api.Features.Reviews.Requests.cs;

using Application.Common;
using Application.Reviews;
using Application.Reviews.Comments;
using Application.Reviews.Create;
using Application.Reviews.Get;
using Application.Reviews.GetByMovieId;
using Application.Reviews.GetByUserIdQuery;
using Application.Reviews.GetDetailsQuery;
using Application.Reviews.GetOwnedByMovieId;
using Application.Reviews.Likes;
using Application.Reviews.Update;

using Domain.Movies.Reviews.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Reviews;

public static class ReviewsEndpoint
{
    public const string Uri = "api/reviews";
    public const string Get = "/";
    public const string Create = "/";
    public const string Update = "/";

    public static void MapReviews(this WebApplication app)
    {
        var reviews = app.MapGroup(Uri).RequireAuthorization();

        reviews.MapGet(Get, GetReviewsAsync);
        reviews.MapGet("/{movieId}", GetReviewsByMovieIdAsync).AllowAnonymous();
        reviews.MapGet("/details/{reviewId}", GetReviewDetailsByIdAsync).AllowAnonymous();
        reviews.MapGet("/own/{movieId}", GetOwnedReviewsByMovieIdAsync);
        reviews.MapGet("/user/{userId}", GetReviewsByUserIdAsync).AllowAnonymous();

        reviews.MapPost(Create, CreateReviewAsync).WithName("Created");
        reviews.MapPost("/like/{reviewId}", LikeReviewAsync);
        reviews.MapPost("/comment/{reviewId}", CommentReviewAsync);

        reviews.MapPut(Update, UpdateReviewAsync);

        reviews.MapDelete("/{id}", (Guid id) => TypedResults.Ok($"delete {id}")); // TODO
    }

    private static async Task<IResult> CommentReviewAsync(Guid reviewId, [FromBody] string comment, ISender mediatr, CancellationToken cancellationToken)
        => Response<Unit>.Match(await mediatr.Send(new CommentReviewCommand(
            ReviewId.Create(reviewId),
            comment),
            cancellationToken));

    private static async Task<IResult> LikeReviewAsync(Guid reviewId, ISender mediatr, CancellationToken cancellationToken)
        => Response<ReviewResponse>.Match(await mediatr.Send(new LikeReviewCommand(
            ReviewId.Create(reviewId)),
            cancellationToken));

    private static async Task<IResult> GetReviewDetailsByIdAsync(Guid reviewId, ISender mediatr, CancellationToken cancellationToken)
        => Response<ReviewDetailsResponse>.Match(await mediatr.Send(new GetReviewDetailsByIdQuery(reviewId), cancellationToken));

    private static async Task<IResult> GetReviewsAsync(int page, ReviewsBy? reviewsBy, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<ReviewResponse>>.Match(await mediatr.Send(new GetReviewsQuery(page, reviewsBy ?? ReviewsBy.Latest), cancellationToken));

    private static async Task<IResult> GetReviewsByMovieIdAsync(int movieId, int page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<ReviewResponse>>.Match(await mediatr.Send(new GetReviewsByMovieIdQuery(movieId, page), cancellationToken));

    private static async Task<IResult> GetOwnedReviewsByMovieIdAsync(int movieId, ISender mediatr, CancellationToken cancellationToken)
        => Response<ReviewResponse>.Match(await mediatr.Send(new GetOwnedReviewsByMovieIdQuery(movieId), cancellationToken));

    private static async Task<IResult> GetReviewsByUserIdAsync(string userId, ISender mediatr, CancellationToken cancellationToken, int page = 1)
        => Response<Paged<ReviewWithMovieDetailsResponse>>.Match(await mediatr.Send(new GetReviewsByUserIdQuery(Guid.Parse(userId), page), cancellationToken));

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

    private static async Task<IResult> UpdateReviewAsync(
        UpdateReviewRequest request,
        ISender mediatr,
        CancellationToken cancellationToken)
        => Response<ReviewId>.Match(await mediatr.Send(new UpdateReviewCommand(
            request.ReviewId,
            request.Rating,
            request.Text,
            request.ContainsSpoilers),
        cancellationToken));
}
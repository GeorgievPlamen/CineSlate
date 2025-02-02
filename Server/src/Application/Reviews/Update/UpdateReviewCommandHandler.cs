using System.Security.Claims;

using Application.Common;
using Application.Reviews.Interfaces;

using Domain.Common;
using Domain.Movies.Reviews.Errors;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Application.Reviews.Update;

public class UpdateReviewCommandHandler(
    IReviewRepository reviewRepository,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateReviewCommand, Result<ReviewId>>
{
    public async Task<Result<ReviewId>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
            return Result<ReviewId>.Failure(Error.ServerError());

        var userId = UserId.Create(
            Guid.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!));
        if (userId is null)
            return Result<ReviewId>.Failure(UserErrors.NotFound());

        var reviewId = ReviewId.Create(request.ReviewId);
        var userRevivew = await reviewRepository
            .GetReviewByIdAsync(
                reviewId,
                cancellationToken);

        if (userRevivew is null)
            return Result<ReviewId>.Failure(ReviewErrors.NotFound(ReviewId.Create(request.ReviewId)));

        if (userRevivew.Author != userId)
            return Result<ReviewId>.Failure(ReviewErrors.NotByUser(userId.Value.ToString()));

        var success = await reviewRepository.UpdateAsync(
            reviewId,
            request.Rating,
            request.Text ?? "",
            request.ContainsSpoilers
            , cancellationToken);

        if (!success)
            return Result<ReviewId>.Failure(Error.ServerError());

        return Result<ReviewId>.Success(ReviewId.Create(request.ReviewId));
    }
}

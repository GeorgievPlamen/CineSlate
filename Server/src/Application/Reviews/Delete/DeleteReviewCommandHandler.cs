using Application.Common;
using Application.Common.Interfaces;
using Application.Reviews.Interfaces;

using Domain.Common;
using Domain.Movies.Reviews.Errors;

using MediatR;

namespace Application.Reviews.Delete;

public class DeleteReviewCommandHandler(
    IReviewRepository reviewRepository,
    IAppContext appContext) : IRequestHandler<DeleteReviewCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();
        var review = await reviewRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);

        if (review is null)
            return Result<Unit>.Failure(ReviewErrors.NotFound(request.ReviewId));

        if (review.Author != userId)
            return Result<Unit>.Failure(ReviewErrors.NotByUser(userId.Value.ToString()));

        var success = await reviewRepository.DeleteAsync(request.ReviewId, cancellationToken);

        return success
            ? Result<Unit>.Success(new())
            : Result<Unit>.Failure(Error.ServerError());
    }
}

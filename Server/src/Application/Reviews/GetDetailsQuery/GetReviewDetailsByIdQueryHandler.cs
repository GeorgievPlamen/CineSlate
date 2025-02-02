

using Application.Common;
using Application.Reviews.Interfaces;

using Domain.Movies.Reviews.Errors;
using Domain.Movies.Reviews.ValueObjects;

using MediatR;

namespace Application.Reviews.GetDetailsQuery;

public class GetReviewDetailsByIdQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetReviewDetailsByIdQuery, Result<ReviewDetailsResponse>>
{

    public async Task<Result<ReviewDetailsResponse>> Handle(GetReviewDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var id = ReviewId.Create(request.ReviewId);
        var result = await reviewRepository.GetReviewByIdAsync(id, cancellationToken);

        return result is null
            ? Result<ReviewDetailsResponse>.Failure(ReviewErrors.NotFound(id))
            : Result<ReviewDetailsResponse>.Success(result.ToDetailsResponse());
    }
}

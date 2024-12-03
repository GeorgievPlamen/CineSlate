using Application.Common;
using Application.Movies.Interfaces;
using Application.Reviews.Interfaces;
using MediatR;

namespace Application.Reviews.Get;

public class GetReviewsQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetReviewsQuery, Result<Paged<ReviewResponse>>>
{
    public async Task<Result<Paged<ReviewResponse>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var pagedReviews = await reviewRepository.GetReviewsAsync(request.Page, request.ReviewsBy, Paged.DefaultSize, cancellationToken);

        var reviewResponses = pagedReviews.Values.Select(r => r.ToResponse()).ToList();

        return Result<Paged<ReviewResponse>>.Success(new Paged<ReviewResponse>(
            reviewResponses,
            pagedReviews.CurrentPage,
            pagedReviews.HasNextPage,
            pagedReviews.HasNextPage,
            pagedReviews.TotalCount));
    }
}

using Application.Common;
using Application.Reviews.Interfaces;
using Application.Users.Interfaces;
using MediatR;

namespace Application.Reviews.Get;

public class GetReviewsQueryHandler(IReviewRepository reviewRepository, IUserRepository userRepository) : IRequestHandler<GetReviewsQuery, Result<Paged<ReviewResponse>>>
{
    public async Task<Result<Paged<ReviewResponse>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var pagedReviews = await reviewRepository.GetReviewsAsync(request.Page, request.ReviewsBy, Paged.DefaultSize, cancellationToken);

        var users = await userRepository.GetManyByIdAsync(pagedReviews.Values.Select(r => r.Author), cancellationToken);

        var reviewResponses = pagedReviews.Values
            .Select(r => r.ToResponse(
                users.FirstOrDefault(u => u.Id.Value == r.Author.Value)?
                .Username.Value ?? "Username not found"))
            .ToList();

        return Result<Paged<ReviewResponse>>.Success(new Paged<ReviewResponse>(
            reviewResponses,
            pagedReviews.CurrentPage,
            pagedReviews.HasNextPage,
            pagedReviews.HasNextPage,
            pagedReviews.TotalCount));
    }
}

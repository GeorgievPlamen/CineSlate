using Application.Common;
using MediatR;

namespace Application.Reviews.Get;

public record GetReviewsQuery(int Page, ReviewsBy ReviewsBy) : IRequest<Result<Paged<ReviewResponse>>>;
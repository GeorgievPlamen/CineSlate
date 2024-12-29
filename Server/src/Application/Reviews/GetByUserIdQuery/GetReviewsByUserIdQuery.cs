using Application.Common;
using MediatR;

namespace Application.Reviews.GetByUserIdQuery;

public record GetReviewsByUserIdQuery(Guid UserId, int Page) : IRequest<Result<Paged<ReviewWithMovieDetailsResponse>>>;
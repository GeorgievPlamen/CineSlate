using Application.Common;

using MediatR;

namespace Application.Reviews.GetDetailsQuery;

public record GetReviewDetailsByIdQuery(Guid ReviewId) : IRequest<Result<ReviewDetailsResponse>>;
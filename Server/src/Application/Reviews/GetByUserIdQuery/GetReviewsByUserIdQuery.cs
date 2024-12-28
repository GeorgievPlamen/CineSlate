using Application.Common;
using Domain.Users.ValueObjects;
using MediatR;

namespace Application.Reviews.GetByUserIdQuery;

public record GetReviewsByUserIdQuery(Guid UserId, int Page) : IRequest<Result<Paged<ReviewResponse>>>;
using Application.Common;

using Domain.Movies.Reviews.ValueObjects;

using MediatR;

namespace Application.Reviews.Delete;

public record DeleteReviewCommand(ReviewId ReviewId) : IRequest<Result<Unit>>;

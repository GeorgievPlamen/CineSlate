using Application.Common;

using Domain.Movies.Reviews.ValueObjects;

using MediatR;

namespace Application.Reviews.Likes;

public record LikeReviewCommand(ReviewId ReviewId) : IRequest<Result<ReviewDetailsResponse>>;
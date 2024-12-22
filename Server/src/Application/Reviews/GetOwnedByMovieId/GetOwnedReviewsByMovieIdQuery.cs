using Application.Common;
using MediatR;

namespace Application.Reviews.GetOwnedByMovieId;

public record GetOwnedReviewsByMovieIdQuery(int MovieId) : IRequest<Result<ReviewResponse>>;
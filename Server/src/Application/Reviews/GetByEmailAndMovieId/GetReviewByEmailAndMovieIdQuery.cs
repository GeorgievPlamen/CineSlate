using Application.Common;
using MediatR;

namespace Application.Reviews.GetByEmailAndMovieId;

public record GetReviewByEmailAndMovieIdQuery(int MovieId) : IRequest<Result<ReviewResponse>>;
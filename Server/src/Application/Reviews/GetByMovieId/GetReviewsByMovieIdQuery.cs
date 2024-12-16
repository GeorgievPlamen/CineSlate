using Application.Common;
using MediatR;

namespace Application.Reviews.GetByMovieId;

public record GetReviewsByMovieIdQuery(int MovieId, int Page) : IRequest<Result<Paged<ReviewResponse>>>;
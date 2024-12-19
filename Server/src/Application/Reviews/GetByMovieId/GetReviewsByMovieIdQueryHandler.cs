using Application.Common;
using Application.Reviews.Interfaces;
using Domain.Common;
using Domain.Movies.ValueObjects;
using MediatR;

namespace Application.Reviews.GetByMovieId;

public class GetReviewsByMovieIdQueryHandler(IReviewRepository reviewRepository) : IRequestHandler<GetReviewsByMovieIdQuery, Result<Paged<ReviewResponse>>>
{

    public async Task<Result<Paged<ReviewResponse>>> Handle(GetReviewsByMovieIdQuery request, CancellationToken cancellationToken)
    {
        var result = await reviewRepository.GetReviewsByMovieIdAsync(MovieId.Create(request.MovieId), request.Page, Paged.DefaultSize, cancellationToken);

        if (result is null)
            return Result<Paged<ReviewResponse>>.Failure(Error.NotFound());

        var reviewResponses = result.Values.Select(r => r.ToResponse()).ToList();

        return Result<Paged<ReviewResponse>>.Success(new Paged<ReviewResponse>(
            reviewResponses,
            result.CurrentPage,
            result.HasNextPage,
            result.HasPreviousPage,
            result.TotalCount));
    }
}

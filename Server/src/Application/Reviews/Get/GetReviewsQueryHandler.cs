using Application.Common;
using Application.Movies.Interfaces;
using Application.Reviews.Interfaces;
using Domain.Common;
using MediatR;

namespace Application.Reviews.Get;

public class GetReviewsQueryHandler(IReviewRepository reviewRepository, IMovieRepository movieRepository) : IRequestHandler<GetReviewsQuery, Result<Paged<ReviewResponse>>>
{
    public async Task<Result<Paged<ReviewResponse>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var pagedReviews = await reviewRepository.GetReviewsAsync(request.Page, request.ReviewsBy, Paged.DefaultSize, cancellationToken);

        var movieIds = pagedReviews.Values.Select(r => r.MovieId).ToList();

        var movies = await movieRepository.GetManyByIdsAsync(movieIds, cancellationToken);

        if (pagedReviews.Values.Count != movieIds.Count)
            return Result<Paged<ReviewResponse>>.Failure(Error.ServerError());

        var reviewResponses = new List<ReviewResponse>();

        foreach (var review in pagedReviews.Values)
        {
            var movieMatch = movies.First(m => m.Id == review.MovieId);

            reviewResponses.Add(new ReviewResponse(
                review.Rating,
                review.Text,
                review.MovieId.Value,
                movieMatch.Title,
                movieMatch.ReleaseDate,
                movieMatch.PosterPath,
                review.Author.Value,
                review.ContainsSpoilers
            ));
        }

        return Result<Paged<ReviewResponse>>.Success(new Paged<ReviewResponse>(
            reviewResponses,
            pagedReviews.CurrentPage,
            pagedReviews.HasNextPage,
            pagedReviews.HasNextPage,
            pagedReviews.TotalCount));
    }
}

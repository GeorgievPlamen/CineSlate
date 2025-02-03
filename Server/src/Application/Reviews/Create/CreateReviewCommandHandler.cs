using Application.Common;
using Application.Common.Context;
using Application.Common.Interfaces;
using Application.Movies.Interfaces;
using Application.Reviews.Interfaces;

using Domain.Common;
using Domain.Movies.Errors;
using Domain.Movies.Reviews;
using Domain.Movies.Reviews.Errors;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Movies.ValueObjects;

using MediatR;

namespace Application.Reviews.Create;

public class CreateReviewCommandHandler(
    IMovieRepository movieRepository,
    IReviewRepository reviewRepository,
    IAppContext appContext) : IRequestHandler<CreateReviewCommand, Result<ReviewId>>
{
    public async Task<Result<ReviewId>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = appContext.GetUserId();

        var movieId = MovieId.Create(request.MovieId);
        var movie = await movieRepository.GetByIdAsync(movieId, cancellationToken);
        if (movie is null)
            return Result<ReviewId>.Failure(MovieErrors.NotFound(movieId));

        var userRevivew = await reviewRepository
            .GetReviewByAuthorIdAndMovieIdAsync(
                userId,
                movieId,
                cancellationToken);

        if (userRevivew is not null)
            return Result<ReviewId>.Failure(ReviewErrors.UserAlreadyReviewed(userId?.ToString() ?? "User Not Found"));

        var review = Review.Create(
            request.Rating,
            userId,
            request.Text ?? string.Empty,
            request.ContainsSpoilers);

        movie.AddReview(review);

        var success = await movieRepository.UpdateAsync(movie, cancellationToken);

        if (!success)
            return Result<ReviewId>.Failure(Error.ServerError());

        return Result<ReviewId>.Success(review.Id);
    }
}

using System.Security.Claims;
using Application.Common;
using Application.Movies.Interfaces;
using Application.Reviews.Interfaces;
using Domain.Common;
using Domain.Movies.Errors;
using Domain.Movies.Reviews;
using Domain.Movies.Reviews.Errors;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Movies.ValueObjects;
using Domain.Users.Errors;
using Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Reviews.Create;

public class CreateReviewCommandHandler(
    IMovieRepository movieRepository,
    IReviewRepository reviewRepository,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateReviewCommand, Result<ReviewId>>
{
    public async Task<Result<ReviewId>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
            return Result<ReviewId>.Failure(Error.ServerError());

        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
            return Result<ReviewId>.Failure(UserErrors.NotFound());

        var movieId = MovieId.Create(request.MovieId);
        var movie = await movieRepository.GetByIdAsync(movieId, cancellationToken);
        if (movie is null)
            return Result<ReviewId>.Failure(MovieErrors.NotFound(movieId));

        var userRevivew = await reviewRepository
            .GetReviewByAuthorIdAndMovieIdAsync(
                UserId.Create(Guid.Parse(userId)),
                movieId,
                cancellationToken);

        if (userRevivew is not null)
            return Result<ReviewId>.Failure(ReviewErrors.UserAlreadyReviewed(userId));

        var review = Review.Create(
            request.Rating,
            UserId.Create(Guid.Parse(userId)),
            request.Text,
            request.ContainsSpoilers);

        movie.AddReview(review);

        var success = await movieRepository.UpdateAsync(movie, cancellationToken);

        if (!success)
            return Result<ReviewId>.Failure(Error.ServerError());

        return Result<ReviewId>.Success(review.Id);
    }
}

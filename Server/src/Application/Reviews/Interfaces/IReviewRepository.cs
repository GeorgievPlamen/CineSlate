using Application.Common;
using Domain.Movies.Reviews;
using Domain.Movies.ValueObjects;

namespace Application.Reviews.Interfaces;

public interface IReviewRepository
{
    public Task<Paged<Review>> GetReviewsAsync(int page, ReviewsBy reviewsBy, int count, CancellationToken cancellationToken);
    public Task<Paged<Review>> GetReviewsByMovieIdAsync(MovieId movieId, int page, int count, CancellationToken cancellationToken);
}
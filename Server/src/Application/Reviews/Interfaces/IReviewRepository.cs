using Application.Common;
using Domain.Movies.Reviews;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;

namespace Application.Reviews.Interfaces;

public interface IReviewRepository
{
    public Task<Paged<Review>> GetReviewsAsync(int page, ReviewsBy reviewsBy, int count, CancellationToken cancellationToken);
    public Task<Paged<Review>> GetReviewsByMovieIdAsync(MovieId movieId, int page, int count, CancellationToken cancellationToken);
    public Task<Review?> GetReviewByIdAsync(ReviewId reviewId, CancellationToken cancellationToken);
    public Task<Review?> GetReviewByAuthorIdAndMovieIdAsync(UserId userId, MovieId movieId, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(ReviewId reviewId, int rating, string text, bool containsSpoilers, CancellationToken cancellationToken);
}
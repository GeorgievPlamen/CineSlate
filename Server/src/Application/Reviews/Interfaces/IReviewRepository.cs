using Application.Common;
using Domain.Movies.Reviews;

namespace Application.Reviews.Interfaces;

public interface IReviewRepository
{
    public Task<Paged<Review>> GetReviewsAsync(int page, ReviewsBy reviewsBy, int count, CancellationToken cancellationToken);
}
using Application.Common;
using Application.Reviews;
using Application.Reviews.Interfaces;
using Domain.Movies.Reviews;
using Infrastructure.Database;
using Infrastructure.Repositories.MappingExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReviewRepository(CineSlateContext dbContext) : IReviewRepository
{
    public async Task<Paged<Review>> GetReviewsAsync(int page, ReviewsBy reviewsBy, int count, CancellationToken cancellationToken)
    {
        var result = await dbContext.Reviews
            .AsNoTracking()
            .Include(r => r.Movie)
            .Take(count)
            .Skip(count * (page - 1))
            .ToListAsync(cancellationToken);

        var total = await dbContext.Reviews.CountAsync(cancellationToken);

        return new Paged<Review>(
            result.Select(r => r.Unwrap()).ToList(),
            page,
            total - (page * count) > 0,
            page > 1,
            total);
    }
}

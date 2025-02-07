
using Application.Common;
using Application.Reviews;
using Application.Reviews.Interfaces;

using Domain.Movies.Reviews;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;

using Infrastructure.Database;
using Infrastructure.Repositories.MappingExtensions;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReviewRepository(CineSlateContext dbContext) : IReviewRepository
{
    public async Task<Paged<Review>> GetReviewsAsync(int page, ReviewsBy reviewsBy, int count, CancellationToken cancellationToken)
    {
        // TODO order by latest/popular based on reviewsBy

        var result = await dbContext.Reviews
            .AsNoTracking()
            .Include(r => r.Movie)
            .Include(r => r.Likes)
            .OrderBy(r => r.CreatedAt)
            .Take(count)
            .Skip(count * (page - 1))
            .ToListAsync(cancellationToken);

        var total = await dbContext.Reviews.CountAsync(cancellationToken);

        return new Paged<Review>(
            result.Select(r => r.Unwrap(r.Likes.Unwrap())).ToList(),
            page,
            total - (page * count) > 0,
            page > 1,
            total);
    }

    public async Task<Paged<Review>> GetReviewsByMovieIdAsync(MovieId movieId, int page, int count, CancellationToken cancellationToken)
    {
        var result = await dbContext.Reviews
            .AsNoTracking()
            .Include(r => r.Movie)
            .Include(r => r.Likes)
            .Where(r => r.Movie.Id == movieId.Value)
            .OrderBy(r => r.CreatedAt)
            .Take(count)
            .Skip(count * (page - 1))
            .ToListAsync(cancellationToken);

        var total = await dbContext.Reviews
            .Where(r => r.Movie.Id == movieId.Value)
            .CountAsync(cancellationToken);

        return new Paged<Review>(
            result.Select(r => r.Unwrap(r.Likes.Unwrap())).ToList(),
            page,
            total - (page * count) > 0,
            page > 1,
            total);
    }

    public async Task<Paged<Review>> GetReviewsByAuthorIdAsync(UserId userId, int page, int count, CancellationToken cancellationToken)
    {
        var result = await dbContext.Reviews
            .AsNoTracking()
            .Include(r => r.Movie)
            .Include(r => r.Likes)
            .Where(r => r.AuthorId == userId.Value)
            .OrderBy(r => r.CreatedAt)
            .Take(count)
            .Skip(count * (page - 1))
            .ToListAsync(cancellationToken);

        var total = await dbContext.Reviews
            .Where(r => r.AuthorId == userId.Value)
            .CountAsync(cancellationToken);

        return new Paged<Review>(
            result.Select(r => r.Unwrap(r.Likes.Unwrap())).ToList(),
            page,
            total - (page * count) > 0,
            page > 1,
            total);
    }

    public async Task<Review?> GetReviewByAuthorIdAndMovieIdAsync(UserId userId, MovieId movieId, CancellationToken cancellationToken)
    {
        var result = await dbContext.Reviews
            .AsNoTracking()
            .Include(r => r.Movie)
            .Include(r => r.Likes)
            .FirstOrDefaultAsync(x => x.AuthorId == userId.Value && x.Movie.Id == movieId.Value, cancellationToken);

        return result?.Unwrap(result.Likes.Unwrap());
    }

    public async Task<Review?> GetReviewByIdAsync(ReviewId reviewId, CancellationToken cancellationToken)
    {
        var result = await dbContext.Reviews
            .AsNoTracking()
            .Include(r => r.Movie)
            .Include(r => r.Likes)
            .Include(r => r.Comments)
            .FirstOrDefaultAsync(x => x.Id == reviewId.Value, cancellationToken);

        return result?.Unwrap(result.Likes.Unwrap(), result.Comments.Unwrap());
    }

    public async Task<bool> UpdateAsync(ReviewId reviewId, int rating, string text, bool containsSpoilers, CancellationToken cancellationToken)
    {
        var oldReview = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId.Value, cancellationToken);

        if (oldReview is null)
            return false;

        oldReview.Rating = rating;
        oldReview.Text = text;
        oldReview.ContainsSpoilers = containsSpoilers;

        dbContext.Update(oldReview);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateLikesAsync(ReviewId reviewId, List<Like> likes, CancellationToken cancellationToken)
    {
        var oldReview = await dbContext.Reviews.Include(x => x.Likes).FirstOrDefaultAsync(r => r.Id == reviewId.Value, cancellationToken);

        if (oldReview is null)
            return false;

        oldReview.Likes = likes.Unwrap(oldReview);

        dbContext.Update(oldReview);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateCommentsAsync(ReviewId reviewId, List<Comment> comments, CancellationToken cancellationToken)
    {
        var oldReview = await dbContext.Reviews.Include(x => x.Comments).FirstOrDefaultAsync(r => r.Id == reviewId.Value, cancellationToken);

        if (oldReview is null)
            return false;

        oldReview.Comments = comments.Unwrap(oldReview);

        dbContext.Update(oldReview);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}

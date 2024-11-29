using Application.Movies.Interfaces;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository(CineSlateContext dbContext) : IMovieRepository
{
    public async Task<bool> CreateManyAsync(IEnumerable<MovieAggregate> movies, CancellationToken cancellationToken)
    {
        dbContext.AddRange(movies);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<MovieAggregate?> GetByIdAsync(MovieId id, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<List<MovieAggregate>> GetManyByIdsAsync(IEnumerable<MovieId> ids, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<bool> UpdateAsync(MovieAggregate movie, CancellationToken cancellationToken)
    {
        dbContext.Update(movie);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}

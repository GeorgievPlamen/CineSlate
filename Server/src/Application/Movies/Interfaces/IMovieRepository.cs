using Domain.Movies;
using Domain.Movies.ValueObjects;

namespace Application.Movies.Interfaces;

public interface IMovieRepository
{
    Task<MovieAggregate?> GetByIdAsync(MovieId id, CancellationToken cancellationToken);
    Task<List<MovieAggregate>> GetManyByIdsAsync(IEnumerable<MovieId> ids, CancellationToken cancellationToken);
    Task<bool> CreateManyAsync(IEnumerable<MovieAggregate> movies, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(MovieAggregate movie, CancellationToken cancellationToken);
}
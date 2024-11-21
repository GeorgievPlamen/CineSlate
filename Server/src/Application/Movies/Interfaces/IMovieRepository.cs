using Domain.Movies;
using Domain.Movies.ValueObjects;

namespace Application.Movies.Interfaces;

public interface IMovieRepository
{
    Task<List<MovieAggregate>> GetManyByIdsAsync(IEnumerable<MovieId> ids, CancellationToken cancellationToken);
    Task<bool> CreateManyAsync(IEnumerable<MovieAggregate> movies, CancellationToken cancellationToken);
}
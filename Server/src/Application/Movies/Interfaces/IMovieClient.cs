using Application.Common;

namespace Application.Movies.Interfaces;

public interface IMovieClient
{
    Task<Paged<Movie>> GetMoviesByPageAsync(MoviesBy moviesBy, int pageNumber, CancellationToken cancellationToken);
    Task<MovieDetailed?> GetMovieDetailsAsync(int id, CancellationToken cancellationToken);
}
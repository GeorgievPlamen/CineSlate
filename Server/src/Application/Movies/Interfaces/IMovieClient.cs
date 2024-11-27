using Application.Common;

namespace Application.Movies.Interfaces;

public interface IMovieClient
{
    Task<Paged<Movie>> GetMoviesByPageAsync(MoviesBy moviesBy, int pageNumber);
    Task<MovieFull> GetMovieDetailsAsync(int id);
}
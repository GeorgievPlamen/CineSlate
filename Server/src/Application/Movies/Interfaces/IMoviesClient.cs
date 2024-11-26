using Application.Common;

namespace Application.Movies.Interfaces;

public interface IMoviesClient
{
    Task<Paged<Movie>> GetMoviesByPageAsync(MoviesBy moviesBy, int pageNumber);
    Task<MovieDetails> GetMovieDetailsAsync(int id);
}
using Application.Common;

namespace Application.Movies.Interfaces;

public interface IMoviesClient
{
    Task<Paged<ExternalMovie>> GetPopularMoviesByPage(int pageNumber);
}
using Application.Movies.Interfaces;

namespace Infrastructure.MoviesClient;

public class TMDBClient : IMoviesClient
{
    public void GetPopularMoviesByPage(int pageNumber)
    {
        throw new NotImplementedException();
    }
}

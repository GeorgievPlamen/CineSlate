namespace Application.Movies.Interfaces;

public interface IMoviesClient
{
    void GetPopularMoviesByPage(int pageNumber);
}
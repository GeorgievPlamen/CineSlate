namespace Application.Movies.Interfaces;

public interface IMoviesClient
{
    Task GetPopularMoviesByPage(int pageNumber);
}
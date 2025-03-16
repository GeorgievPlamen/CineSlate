using Application.Common;

namespace Application.Movies.Interfaces;

public interface IMovieClient
{
    Task<Paged<ExternalMovie>> GetMoviesByPageAsync(MoviesBy moviesBy, int pageNumber, CancellationToken cancellationToken);
    Task<ExternalMovieDetailed?> GetMovieDetailsAsync(int id, CancellationToken cancellationToken);
    Task<Paged<ExternalMovie>> GetMoviesByTitle(string searchCriteria, int pageNumber, CancellationToken cancellationToken);
    Task<Paged<ExternalMovie>> GetMoviesByGenreAndYear(int pageNumber, int[]? genreIds, int? year, CancellationToken cancellationToken);
}

public record ExternalMovie(
    int Id,
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    int[] GenreIds);

public record ExternalGenre(
    int Id,
    string Name);

public record ExternalMovieDetailed(
    int Id,
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    ExternalGenre[] Genres,
    string BackdropPath,
    long Budget,
    string Homepage,
    string ImdbId,
    string OriginCountry,
    long Revenue,
    int Runtime,
    string Status,
    string Tagline);
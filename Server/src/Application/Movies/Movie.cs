using Domain.Movies.ValueObjects;

namespace Application.Movies;

public record Movie(
    int Id,
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    IReadOnlyList<Genre> Genres);

public record ExternalMovie(
    int Id,
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    List<int> GenreIds);
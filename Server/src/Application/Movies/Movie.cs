using Domain.Movies.ValueObjects;

namespace Application.Movies;

public record Movie(
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    List<Genre> Genres);
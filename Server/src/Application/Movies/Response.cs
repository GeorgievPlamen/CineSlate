using Domain.Movies.ValueObjects;

namespace Application.Movies;

public record Movie(
    int Id,
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    IReadOnlyList<Genre> Genres);

public record MovieDetails(
    int Id,
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    IReadOnlyList<Genre> Genres,
    string BackdropPath,
    long Budget,
    string Homepage,
    string ImdbId,
    string OriginCountry,
    long Revenue,
    int Runtime,
    string Status,
    string Tagline);
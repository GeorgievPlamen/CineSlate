using Domain.Movies;
using Domain.Movies.ValueObjects;

namespace Application.Movies;

public record Movie(
    int Id,
    string Title,
    string Description,
    DateOnly ReleaseDate,
    string PosterPath,
    IReadOnlyList<Genre> Genres);

public record MovieFull(
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


public static class Converter
{
    public static Movie ToMovie(this MovieAggregate movie) => new(
            movie.Id.Value,
            movie.Title,
            movie.Description,
            movie.ReleaseDate,
            movie.PosterPath,
            movie.Genres);
    public static MovieFull ToMovieFull(this MovieAggregate movie) => new(
            movie.Id.Value,
            movie.Title,
            movie.Description,
            movie.ReleaseDate,
            movie.PosterPath,
            movie.Genres,
            movie.Details?.BackdropPath ?? "",
            movie.Details?.Budget ?? 0,
            movie.Details?.Homepage ?? "",
            movie.Details?.ImdbId ?? "",
            movie.Details?.OriginCountry ?? "",
            movie.Details?.Revenue ?? 0,
            movie.Details?.Runtime ?? 0,
            movie.Details?.Status ?? "",
            movie.Details?.Tagline ?? "");
}
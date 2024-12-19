using Domain.Movies;
using Domain.Movies.ValueObjects;

namespace Application.Movies;

public record Movie(
    int Id,
    string Title,
    DateOnly ReleaseDate,
    string PosterPath,
    double Rating);

public record MovieDetailed(
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
    string Tagline,
    double Rating);


public static class Converter
{
    public static Movie ToMovie(this MovieAggregate movie) => new(
            movie.Id.Value,
            movie.Title,
            movie.ReleaseDate,
            movie.PosterPath,
            movie.Rating);
    public static MovieDetailed ToMovieDetailed(this MovieAggregate movie) => new(
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
            movie.Details?.Tagline ?? "",
            movie.Rating);
}
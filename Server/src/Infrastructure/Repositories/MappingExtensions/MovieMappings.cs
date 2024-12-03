using Domain.Movies;
using Domain.Movies.ValueObjects;
using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class MovieMappings
{
    public static MovieModel ToModel(this MovieAggregate movie, IEnumerable<GenreModel> genres)
    {
        var genresLookup = genres.ToDictionary(g => g.Id);

        return new()
        {
            Id = movie.Id.Value,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            PosterPath = movie.PosterPath,
            BackdropPath = movie.Details.BackdropPath,
            Budget = movie.Details.Budget,
            Homepage = movie.Details.Homepage,
            ImdbId = movie.Details.ImdbId,
            OriginCountry = movie.Details.OriginCountry,
            Revenue = movie.Details.Revenue,
            Runtime = movie.Details.Runtime,
            Status = movie.Details.Status,
            Tagline = movie.Details.Tagline,
            Genres = [.. movie.Genres.Select(g => genresLookup[g.Id])]
        };
    }

    public static MovieAggregate Unwrap(this MovieModel model)
        => MovieAggregate.Create(
        MovieId.Create(model.Id),
        model.Title,
        model.Description,
        model.ReleaseDate,
        model.PosterPath,
        model.Genres.Select(g => Genre.Create(g.Id, g.Name)),
        MovieDetails.Create(
            model.BackdropPath,
            model.Budget,
            model.Homepage,
            model.ImdbId,
            model.OriginCountry,
            model.Revenue,
            model.Runtime,
            model.Status,
            model.Tagline),
        model.Rating);
}
using Application.Movies.Interfaces;
using Domain.Movies;
using Domain.Movies.ValueObjects;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository(CineSlateContext dbContext) : IMovieRepository
{
    public async Task<bool> CreateManyAsync(IEnumerable<MovieAggregate> movies, CancellationToken cancellationToken)
    {
        var genreIds = movies
            .SelectMany(m => m.Genres)
            .Select(g => g.Id);

        var genres = await dbContext.Genres
            .Where(g => genreIds.Contains(g.Id))
            .ToDictionaryAsync(g => g.Id, cancellationToken);

        var models = movies.Select(m => ToModel(m, genres));

        dbContext.AddRange(models);

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<MovieAggregate?> GetByIdAsync(MovieId id, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public async Task<List<MovieAggregate>> GetManyByIdsAsync(IEnumerable<MovieId> ids, CancellationToken cancellationToken)
        => throw new NotImplementedException();
    // TODO
    // await dbContext.Movies
    //     .Where(m => ids
    //         .Select(id => id.Value)
    //         .ToList()
    //         .Contains(m.Id))
    //     .ToListAsync(cancellationToken).to;

    public async Task<bool> UpdateAsync(MovieAggregate movie, CancellationToken cancellationToken)
    {
        dbContext.Update(movie);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    private static MovieModel ToModel(MovieAggregate movie, IDictionary<int, GenreModel> genres)
        => new()
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
            Genres = [.. movie.Genres.Select(mg => genres[mg.Id])],
        };

    // TODO
    // private static MovieAggregate FromModel(MovieModel movie)
    //     => new()
    //     {
    //         Id = movie.Id.Value,
    //         Title = movie.Title,
    //         Description = movie.Description,
    //         ReleaseDate = movie.ReleaseDate,
    //         PosterPath = movie.PosterPath,
    //         BackdropPath = movie.Details.BackdropPath,
    //         Budget = movie.Details.Budget,
    //         Homepage = movie.Details.Homepage,
    //         ImdbId = movie.Details.ImdbId,
    //         OriginCountry = movie.Details.OriginCountry,
    //         Revenue = movie.Details.Revenue,
    //         Runtime = movie.Details.Runtime,
    //         Status = movie.Details.Status,
    //         Tagline = movie.Details.Tagline,
    //         Genres = [.. movie.Genres.Select(mg => genres[mg.Id])],
    //     };
}

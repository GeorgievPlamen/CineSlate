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
        var genreIds = movies.SelectMany(m => m.Genres.Select(g => g.Id)).Distinct().ToList();

        var existingGenres = await dbContext.Genres
            .Where(g => genreIds.Contains(g.Id))
            .ToListAsync(cancellationToken);

        bool MissingGenresInDb = existingGenres.Count != genreIds.Count;

        if (MissingGenresInDb)
        {
            var missingGenreIds = genreIds.Except(existingGenres.Select(eg => eg.Id));

            dbContext.AddRange(missingGenreIds
                .Select(id => new GenreModel() { Id = id }));

            await dbContext.SaveChangesAsync(cancellationToken);

            var createdGenres = await dbContext.Genres
                .Where(g => missingGenreIds.Contains(g.Id))
                .ToListAsync(cancellationToken);

            existingGenres.AddRange(createdGenres);
        }

        var genresLookup = existingGenres.ToDictionary(g => g.Id);

        dbContext.AddRange(movies.Select(m => ToModel(m, genresLookup)));

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<MovieAggregate?> GetByIdAsync(MovieId id, CancellationToken cancellationToken)
    {
        var model = await dbContext.Movies
            .AsNoTracking()
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == id.Value, cancellationToken);

        if (model is null)
            return null;

        return FromModel(model);
    }

    public async Task<List<MovieAggregate>> GetManyByIdsAsync(IEnumerable<MovieId> ids, CancellationToken cancellationToken)
    {
        var idValues = ids.Select(id => id.Value).ToList();

        var models = await dbContext.Movies
            .AsNoTracking()
            .Where(m => idValues.Contains(m.Id))
            .Include(m => m.Genres)
            .ToListAsync(cancellationToken);

        var movieAggregates = models
            .Select(FromModel)
            .ToList();

        return movieAggregates;
    }

    public async Task<bool> UpdateAsync(MovieAggregate movie, CancellationToken cancellationToken)
    {
        var model = await dbContext.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.Id == movie.Id.Value, cancellationToken);

        if (model is null)
            return false;

        UpdateModel(model, movie);

        dbContext.Update(model);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    private static void UpdateModel(MovieModel model, MovieAggregate movie)
    {
        model.Id = movie.Id.Value;
        model.Title = movie.Title;
        model.Description = movie.Description;
        model.ReleaseDate = movie.ReleaseDate;
        model.PosterPath = movie.PosterPath;
        model.BackdropPath = movie.Details.BackdropPath;
        model.Budget = movie.Details.Budget;
        model.Homepage = movie.Details.Homepage;
        model.ImdbId = movie.Details.ImdbId;
        model.OriginCountry = movie.Details.OriginCountry;
        model.Revenue = movie.Details.Revenue;
        model.Runtime = movie.Details.Runtime;
        model.Status = movie.Details.Status;
        model.Tagline = movie.Details.Tagline;

        foreach (var genre in model.Genres)
        {
            genre.Name = movie.Genres.First(g => genre.Id == g.Id).Value;
        }
    }

    private static MovieModel ToModel(MovieAggregate movie, Dictionary<int, GenreModel> genres)
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
            Genres = [.. movie.Genres.Select(g => genres[g.Id])]
        };

    private static MovieAggregate FromModel(MovieModel movie)
        => MovieAggregate.Create(
        MovieId.Create(movie.Id),
        movie.Title,
        movie.Description,
        movie.ReleaseDate,
        movie.PosterPath,
        movie.Genres.Select(g => Genre.Create(g.Id, g.Name)),
        MovieDetails.Create(
            movie.BackdropPath,
            movie.Budget,
            movie.Homepage,
            movie.ImdbId,
            movie.OriginCountry,
            movie.Revenue,
            movie.Runtime,
            movie.Status,
            movie.Tagline)
    );
}

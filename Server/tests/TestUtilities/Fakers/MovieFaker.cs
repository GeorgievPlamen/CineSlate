using Application.Movies;
using Application.Movies.Interfaces;
using Bogus;
using Infrastructure.Database.Models;

namespace TestUtilities.Fakers;

public static class MovieFaker
{
    public static List<Movie> GenerateMovies(int howMany = 1) =>
        new Faker<Movie>()
            .CustomInstantiator(f => new Movie
            (
                Id: f.Random.Number(1000, 5000),
                Title: f.Name.FullName(),
                ReleaseDate: f.Date.PastDateOnly(),
                PosterPath: f.Internet.Url(),
                Rating: f.Random.Number(1, 5)
            ))
            .Generate(howMany);

    public static List<MovieModel> GenerateMovieModels(int howMany = 1) =>
        new Faker<MovieModel>()
            .RuleFor(m => m.Id, f => f.Random.Number(1000, 5000))
            .RuleFor(m => m.Title, f => f.Lorem.Word())
            .RuleFor(m => m.Description, f => f.Lorem.Sentence())
            .RuleFor(m => m.ReleaseDate, f => f.Date.PastDateOnly())
            .RuleFor(m => m.PosterPath, f => f.Lorem.Word())
            .RuleFor(m => m.BackdropPath, f => f.Lorem.Word())
            .RuleFor(m => m.Budget, f => f.Random.Number(1000, 5000))
            .RuleFor(m => m.Homepage, f => f.Internet.Url())
            .RuleFor(m => m.ImdbId, f => f.Lorem.Word())
            .RuleFor(m => m.OriginCountry, f => f.Lorem.Word())
            .RuleFor(m => m.Revenue, f => f.Random.Number(1000, 5000))
            .RuleFor(m => m.Runtime, f => f.Random.Number(1000, 5000))
            .RuleFor(m => m.Status, f => f.Lorem.Word())
            .RuleFor(m => m.Tagline, f => f.Lorem.Word())
            .RuleFor(m => m.Rating, f => f.Random.Number(1, 5))
            .Generate(howMany);

    public static List<ExternalMovie> GenerateExternalMovies(int howMany = 1) =>
        new Faker<ExternalMovie>()
            .CustomInstantiator(f => new ExternalMovie
            (
                Id: f.Random.Number(1000, 5000),
                Title: f.Name.FullName(),
                Description: f.Lorem.Sentence(),
                ReleaseDate: f.Date.PastDateOnly(),
                PosterPath: f.Internet.Url(),
                GenreIds: [f.Random.Number(1000, 5000)]
            ))
            .Generate(howMany);

    public static ExternalMovieDetailed GenerateExternalMovieDetails(MovieModel movie) =>
        new Faker<ExternalMovieDetailed>()
            .CustomInstantiator(f => new ExternalMovieDetailed
            (
                Id: movie.Id,
                Title: movie.Title,
                Description: movie.Description,
                ReleaseDate: movie.ReleaseDate,
                PosterPath: movie.PosterPath,
                Genres: [new(movie.Genres.First().Id, f.Lorem.Word())],
                BackdropPath: f.Internet.Url(),
                Budget: f.Random.Number(1000, 1000000),
                Homepage: f.Internet.Url(),
                ImdbId: f.Lorem.Word(),
                OriginCountry: f.Lorem.Word(),
                Revenue: f.Random.Number(1000, 1000000),
                Runtime: f.Random.Number(60, 500),
                Status: f.Lorem.Word(),
                Tagline: f.Lorem.Sentence()
            ))
            .Generate();
}
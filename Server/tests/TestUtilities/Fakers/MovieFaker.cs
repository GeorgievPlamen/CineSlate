using Application.Movies;
using Application.Movies.Interfaces;
using Bogus;
using Domain.Movies.ValueObjects;

namespace TestUtilities.Fakers;

public static class MovieFaker
{
    public static List<Movie> GenerateMovies(int howMany = 1) =>
        new Faker<Movie>()
            .CustomInstantiator(f => new Movie
            (
                Id: f.Random.Number(1000, 5000),
                Title: f.Name.FullName(),
                Description: f.Lorem.Sentence(),
                ReleaseDate: f.Date.PastDateOnly(),
                PosterPath: f.Internet.Url(),
                Genres: [Genre.Create(f.Random.Number(1000, 5000))]
            ))
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

    public static ExternalMovieDetailed GenerateExternalMovieDetails(Movie movie) =>
        new Faker<ExternalMovieDetailed>()
            .CustomInstantiator(f => new ExternalMovieDetailed
            (
                Id: movie.Id,
                Title: movie.Title,
                Description: movie.Description,
                ReleaseDate: movie.ReleaseDate,
                PosterPath: movie.PosterPath,
                Genres: [new(movie.Genres[0].Id, f.Lorem.Word())],
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
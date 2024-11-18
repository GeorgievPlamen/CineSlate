using Application.Movies;
using Bogus;
using Domain.Movies.ValueObjects;

namespace TestUtilities.Fakers;

public static class MovieFaker
{
    public static List<Movie> GenerateMovies(int howMany = 1) =>
        new Faker<Movie>()
            .CustomInstantiator(f => new Movie
            (
                Id: f.Random.Number(),
                Title: f.Name.FullName(),
                Description: f.Lorem.Sentence(),
                ReleaseDate: f.Date.PastDateOnly(),
                PosterPath: f.Internet.Url(),
                Genres: [Genre.Create(f.Random.Number())]
            ))
            .Generate(howMany);

    public static List<ExternalMovie> GenerateExternalMovies(int howMany = 1) =>
        new Faker<ExternalMovie>()
            .CustomInstantiator(f => new ExternalMovie
            (
                Id: f.Random.Number(),
                Title: f.Name.FullName(),
                Description: f.Lorem.Sentence(),
                ReleaseDate: f.Date.PastDateOnly(),
                PosterPath: f.Internet.Url(),
                GenreIds: [f.Random.Number(1, 10)]
            ))
            .Generate(howMany);
}
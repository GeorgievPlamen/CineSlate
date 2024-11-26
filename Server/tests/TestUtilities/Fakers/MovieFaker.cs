using Application.Movies;
using Bogus;
using Domain.Movies;
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
}
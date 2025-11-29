using Bogus;

using Infrastructure.Database.Models;

namespace TestUtilities.Fakers;

public static class WatchlistFaker
{
    public static WatchlistModel GenerateWatchlist(Guid? userId) =>
        new Faker<WatchlistModel>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.MovieToWatchModels, f => [
            new()
            {
                Id = f.Random.Guid(),
                MovieId = f.Random.Number(10,5000),
                Watched = false
            },
            new()
            {
                Id = f.Random.Guid(),
                MovieId = f.Random.Number(10,5000),
                Watched = false
            },
            new()
            {
                Id = f.Random.Guid(),
                MovieId = f.Random.Number(10,5000),
                Watched = false
            },
            new()
            {
                Id = f.Random.Guid(),
                MovieId = f.Random.Number(10,5000),
                Watched = false
            },
            new()
            {
                Id = f.Random.Guid(),
                MovieId = f.Random.Number(10,5000),
                Watched = false
            },])
            .RuleFor(x => x.UserId, f => userId ?? f.Random.Guid())
            .Generate();
}
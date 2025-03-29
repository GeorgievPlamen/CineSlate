using Bogus;

using Infrastructure.Database.Models;

namespace TestUtilities.Fakers;

public static class WatchlistFaker
{
    public static List<WatchlistModel> GenerateWatchlists(int howMany = 1) =>
        new Faker<WatchlistModel>()
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.MovieToWatchModels, f => [new()
            {
                Id = f.Random.Guid(),
                MovieId = f.Random.Number(),
                Watched = false
            }])
            .RuleFor(x => x.UserId, f => f.Random.Guid())
            .Generate(howMany);
}
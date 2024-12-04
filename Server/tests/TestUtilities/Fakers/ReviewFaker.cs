using Bogus;
using Infrastructure.Database.Models;

namespace TestUtilities.Fakers;

public static class ReviewFaker
{
    public static List<ReviewModel> GenerateReviews(int howMany = 1) => new Faker<ReviewModel>()
        .RuleFor(r => r.Id, f => Guid.NewGuid())
        .RuleFor(r => r.Rating, f => f.Random.Number(1, 5))
        .RuleFor(r => r.AuthorId, f => Guid.NewGuid())
        .RuleFor(r => r.Text, f => f.Lorem.Sentence())
        .RuleFor(r => r.ContainsSpoilers, f => f.Random.Bool())
        .Generate(howMany);
}
using Domain.Common.Models;

namespace Domain.Movies.Ratings.ValueObjects;

public class RatingId : ValueObject
{
    private RatingId() { }
    public Guid Value { get; private set; }

    public static RatingId Create() => new() { Value = Guid.NewGuid() };
    public static RatingId Create(RatingId id) => new() { Value = id.Value };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

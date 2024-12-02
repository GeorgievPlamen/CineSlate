using Domain.Common.Models;

namespace Domain.Movies.Reviews.ValueObjects;

public class ReviewId : ValueObject
{
    private ReviewId() { }
    public Guid Value { get; private set; }

    public static ReviewId Create() => new() { Value = Guid.NewGuid() };
    public static ReviewId Create(Guid id) => new() { Value = id };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

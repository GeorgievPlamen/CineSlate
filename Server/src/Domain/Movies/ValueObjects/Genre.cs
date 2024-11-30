using Domain.Common.Models;

namespace Domain.Movies.ValueObjects;

public class Genre : ValueObject
{
    private Genre() { }

    public int Id { get; private set; }
    public string Value { get; private set; } = null!;

    public static Genre Create(int id, string value = "") => new() { Id = id, Value = value };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

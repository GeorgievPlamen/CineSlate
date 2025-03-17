using Domain.Common.Models;

namespace Domain.Users.ValueObjects;

public class UserId : ValueObject
{
    private UserId() { }

    public Guid Value { get; init; }
    public static UserId Create() => new() { Value = Guid.NewGuid() };
    public static UserId Create(Guid value) => new() { Value = Guid.Parse(value.ToString()) };
    public bool HasValue() => Value != Guid.Empty;
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

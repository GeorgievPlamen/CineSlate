using Domain.Common.Models;

namespace Domain.Users.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; init; }

    private UserId() { }
    public static UserId Create() => new() { Value = Guid.NewGuid() };
    public static UserId Create(Guid value) => new() { Value = Guid.Parse(value.ToString()) };
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using Domain.Common.Models;

namespace Domain.Users.ValueObjects;

public class UserId : ValueObject
{
    public Guid Id { get; init; }

    private UserId() { }
    public static UserId Create() => new() { Id = Guid.NewGuid() };
    public static UserId Create(Guid value) => new() { Id = Guid.Parse(value.ToString()) };
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}

using Domain.Common.Models;

namespace Domain.Users.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; init; }

    public UserId()
    {
        Value = Guid.NewGuid();
    }
    public UserId(Guid guid)
    {
        Value = guid;
    }    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

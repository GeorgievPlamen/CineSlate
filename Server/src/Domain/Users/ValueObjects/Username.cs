using Domain.Common.Models;

namespace Domain.Users.ValueObjects;

public class Username : ValueObject
{
    private Username() { }
    public string Value { get; private set; } = null!;
    public string OnlyName { get; private set; } = null!;

    public static Username Create(string userName, UserId userId)
        => new() { Value = $"{userName}#{userId.Value.ToString().Substring(0, 5)}", OnlyName = userName };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

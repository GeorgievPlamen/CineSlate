using Domain.Common.Models;

namespace Domain.Users;

public class Name : ValueObject
{
    private Name() { }
    public string First { get; private set; } = null!;
    public string Last { get; private set; } = null!;

    public static Name Create(string firstName, string lastName)
    => new() { First = firstName, Last = lastName };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return First;
        yield return Last;
    }
}

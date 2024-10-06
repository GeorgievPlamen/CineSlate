using Domain.Common.Models;

namespace Domain.Users;

public class Name : ValueObject
{
    private Name()
    {
    }
    public Name(string firstName, string lastName)
    {
        First = firstName;
        Last = lastName;
    }
    public string First { get; } = null!;
    public string Last { get; } = null!;
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return First;
        yield return Last;
    }
}

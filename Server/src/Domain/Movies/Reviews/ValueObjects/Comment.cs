using Domain.Common.Models;
using Domain.Users.ValueObjects;

namespace Domain.Movies.Reviews.ValueObjects;

public class Comment : ValueObject
{
    public UserId FromUserId { get; private set; } = null!;
    public Username FromUser { get; private set; } = null!;
    public string Value { get; set; } = null!;

    public static Comment Create(UserId userId, Username username, string comment) => new()
    {
        FromUserId = userId,
        FromUser = username,
        Value = comment,
    };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return FromUserId;
        yield return FromUser;
        yield return Value;
    }
}
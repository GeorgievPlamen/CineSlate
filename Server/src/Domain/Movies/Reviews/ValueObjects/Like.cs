using Domain.Common.Models;
using Domain.Users.ValueObjects;

namespace Domain.Movies.Reviews.ValueObjects;

public class Like : ValueObject
{
    public UserId FromUserId { get; private set; } = null!;
    public Username FromUser { get; private set; } = null!;

    public static Like Create(UserId userId, Username username) => new() { FromUserId = userId, FromUser = username };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return FromUserId;
        yield return FromUser;
    }
}

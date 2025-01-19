using Domain.Users.ValueObjects;

namespace Application.Common;

public class RefreshToken
{
    private const int RefreshTokenExpiry = 7;

    public Guid Id { get; private set; }
    public UserId? UserId { get; private set; }
    public string Value { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }

    public static RefreshToken Create(Guid id, UserId userId, string value) => new()
    {
        Id = id,
        UserId = userId,
        Value = value,
        ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenExpiry)
    };

    public static RefreshToken Create(Guid id, UserId userId, string value, DateTime dateTime) => new()
    {
        Id = id,
        UserId = userId,
        Value = value,
        ExpiresAt = dateTime
    };
}


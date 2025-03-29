using Domain.Common.Models;
using Domain.Users.Enums;
using Domain.Users.ValueObjects;
using Domain.Watchlist.ValueObjects;

namespace Domain.Users;

public class User : AggregateRoot<UserId>
{
    private User(UserId id) : base(id) { }

    public Username Username { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? Bio { get; private set; }
    public string? AvatarBase64 { get; private set; }
    public string PasswordHash { get; private set; } = null!;
    public Roles Role { get; private set; }
    public WatchlistId WatchlistId { get; private set; } = null!;

    public static User Create(string userName, string email, string passwordHash, Roles role = Roles.User)
    {
        var id = UserId.Create();

        return new(id)
        {
            Username = Username.Create(userName, id),
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
        };
    }

    public static User Create(UserId userId, string userName, string email, string passwordHash, WatchlistId? watchlistId, string bio = "", Roles role = Roles.User)
    => new(userId)
    {
        Username = Username.Create(userName, userId),
        Email = email,
        PasswordHash = passwordHash,
        Bio = bio,
        Role = role,
        WatchlistId = watchlistId!
    };

    public void UpdateBio(string bio) => Bio = bio;
    public void AddWatchlist(WatchlistId watchlistId) => WatchlistId = watchlistId;
    public void UpdateProfilePicture(string imageBase64) => AvatarBase64 = imageBase64;
}
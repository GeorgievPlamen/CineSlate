using Domain.Common.Models;
using Domain.Users.Enums;
using Domain.Users.ValueObjects;
namespace Domain.Users;

public class User : AggregateRoot<UserId>
{
    private User(UserId id) : base(id) { }

    public Username Username { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Roles Role { get; private set; }

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
}
using Domain.Common.Models;
using Domain.Users.Enums;
using Domain.Users.ValueObjects;

namespace Domain.Users;

public class User : Entity<UserId>
{
    public Name Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Roles Role { get; private set; }

    private User(UserId id) : base(id) {}

    public static User CreateUser(string firstName, string lastName, string email, string passwordHash, Roles role = Roles.User)
        => new(new UserId())
        {
            Name = new(firstName, lastName),
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
        };
}
using Domain.Common.Models;
using Domain.Users.Enums;
using Domain.Users.ValueObjects;
namespace Domain.Users;

public class User : AggregateRoot<UserId>
{
    private User(UserId id) : base(id) { }

    public Name Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Roles Role { get; private set; }

    public static User Create(string firstName, string lastName, string email, string passwordHash, Roles role = Roles.User)
        => new(UserId.Create())
        {
            Name = Name.Create(firstName, lastName),
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
        };
}
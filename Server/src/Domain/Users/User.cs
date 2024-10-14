using Domain.Common.Models;
using Domain.Users.Config;
using Domain.Users.ValueObjects;

namespace Domain.Users;

public class User : Entity<UserId>
{
    public Name Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string Role { get; private set; } = null!;

    private User(UserId id) : base(id)
    {
    }
    public void SetRole(RoleTypes role)
    {
        Role = role switch
        {
            RoleTypes.User => UserRoles.UserRole,
            RoleTypes.Admin => UserRoles.AdminRole,
            _ => UserRoles.UserRole
        };
    }

    public static User CreateUser(string firstName, string lastName, string email, string passwordHash, string role = UserRoles.UserRole)
        => new(new UserId())
        {
            Name = new(firstName, lastName),
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
        };
}
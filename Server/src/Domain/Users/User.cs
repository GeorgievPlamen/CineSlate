using Domain.Common.Models;
using Domain.Users.Config;

namespace Domain.Users;

public class User : Entity
{
    public Name Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string Role { get; private set; } = null!;

    private User()
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
        => new()
        {
            Name = new(firstName, lastName),
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
        };
}
using Domain.Users.Config;

namespace Domain.Users;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; private set; } = UserRoles.UserRole;
    public void SetRole(RoleTypes role)
    {
        Role = role switch
        {
            RoleTypes.User => UserRoles.UserRole,
            RoleTypes.Admin => UserRoles.AdminRole,
            _ => UserRoles.UserRole
        };
    }
}
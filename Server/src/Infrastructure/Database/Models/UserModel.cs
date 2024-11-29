using Domain.Users;
using Domain.Users.Enums;
using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class UserModel : BaseModel
{
    public Guid Id { get; set; }
    public Name Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public Roles Role { get; set; }
}
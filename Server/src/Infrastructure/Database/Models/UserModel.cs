using Domain.Users.Enums;
using Domain.Users.ValueObjects;

using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class UserModel : BaseModel
{
    public Guid Id { get; set; }
    public Username Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Bio { get; set; } = string.Empty;
    public byte[]? AvatarBlob { get; set; }
    public string PasswordHash { get; set; } = null!;
    public Roles Roles { get; set; }
}
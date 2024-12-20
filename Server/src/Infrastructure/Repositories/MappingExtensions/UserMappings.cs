using Domain.Users;
using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class UserMappings
{
    public static UserModel ToModel(this User user)
        => new()
        {
            Id = user.Id.Value,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Roles = user.Role
        };
    public static User Unwrap(this UserModel model)
         => User.Create(
            model.Username.OnlyName,
            model.Email,
            model.PasswordHash,
            model.Roles);
}
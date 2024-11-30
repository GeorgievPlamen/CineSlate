using Domain.Users;
using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class UserMappings
{
    public static UserModel ToModel(this User user)
        => new()
        {
            Id = user.Id.Value,
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Roles = user.Role
        };
    public static User Unwrap(this UserModel model)
         => User.Create(
            model.Name.First,
            model.Name.Last,
            model.Email,
            model.PasswordHash,
            model.Roles);
}
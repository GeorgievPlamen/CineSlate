using Domain.Users;
using Domain.Users.ValueObjects;

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
            Roles = user.Role,
            Bio = user.Bio,
            AvatarBlob = user.AvatarBase64 is not null
                ? Convert.FromBase64String(user.AvatarBase64)
                : null
        };
    public static User Unwrap(this UserModel model)
    {
        var user = User.Create(
               UserId.Create(model.Id),
               model.Username.OnlyName,
               model.Email,
               model.PasswordHash,
               model.Bio ?? "",
               model.Roles);

        if (model.AvatarBlob is not null)
            user.UpdateProfilePicture($"data:image/jpeg;base64,/9j/{Convert.ToBase64String(model.AvatarBlob)}");

        return user;
    }
}
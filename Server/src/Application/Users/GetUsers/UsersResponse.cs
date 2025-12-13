using Domain.Users;

namespace Application.Users.GetUsers;

public record UserResponse(
    string Username,
    Guid Id,
    string Bio,
    string PictureBase64);

public static class Converter
{
    public static UserResponse ToResponse(this User user)
        => new(user.Username.Value, user.Id.Value, user.Bio ?? "", user.AvatarImageBase64 ?? "");
}
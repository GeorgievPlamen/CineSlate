using Domain.Users;

namespace Application.Users.GetUsers;


public record UserResponse(string Username, Guid Id);

public static class Converter
{
    public static UserResponse ToResponse(this User user) => new(user.Username.Value, user.Id.Value);
}
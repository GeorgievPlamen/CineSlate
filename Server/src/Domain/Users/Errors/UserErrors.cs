using Domain.Common;

namespace Domain.Users.Errors;

public static class UserErrors
{
    public static readonly Error AlreadyRegistered = Error.BadRequest("User.AlreadyRegistered", "There is a user already registered with this email.");
    public static readonly Error NotFound = Error.NotFound("User.NotFound", "User with this email not found or password is wrong.");
}
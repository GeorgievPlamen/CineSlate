using Domain.Common;

namespace Domain.Users.Errors;

public static class UserErrors
{
    public static readonly Error AlreadyRegistered = Error.BadRequest("User.AlreadyRegistered", "There is a user already registered with this email.");
    public static readonly Error UserNotFound = Error.NotFound("User.NotFound", "Email not found or password is wrong.");
}
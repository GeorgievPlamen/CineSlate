using Domain.Common;

namespace Domain.Users.Errors;

public static class UserErrors
{
    public static readonly Error AlreadyRegistered = Error.BadRequest("User.AlreadyRegistered", "Can't use the same email.");
    public static readonly Error UserNotFound = Error.NotFound("User.NotFound", "Email not found or password is wrong.");
}
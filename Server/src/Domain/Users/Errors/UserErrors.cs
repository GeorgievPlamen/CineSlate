using Domain.Common;

namespace Domain.Users.Errors;

public static class UserErrors
{
    public static Error AlreadyRegistered(string email) => Error.BadRequest("User.AlreadyRegistered", $"There is a user already registered with email :'{email}'.");
    public static Error NotFound(string email = "unknown") => Error.NotFound("User.NotFound", $"User with email: '{email}', not found or password is wrong.");
    public static Error TokenEpired() => Error.NotAuthorized("User.TokenExpired", $"User's token has expired, please login again.");
}
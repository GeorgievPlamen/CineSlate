using Domain.Common;

namespace Domain.Users.Errors;

public static class UserErrors
{
    public static readonly Error AlreadyRegistered = new("User.AlreadyRegistered", "Can't use the same email.");
}
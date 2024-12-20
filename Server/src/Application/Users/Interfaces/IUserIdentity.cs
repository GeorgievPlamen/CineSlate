using Domain.Users.ValueObjects;

namespace Application.Users.Interfaces;

public interface IUserIdentity
{
    string GenerateJwtToken(UserId userId, Username username, string email, string role);
    string HashPassword(string password);
    bool ValidatePassword(string password, string storedPassword);
}
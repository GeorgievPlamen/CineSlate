namespace Application.Users.Interfaces;

public interface IUserIdentity
{
    string GenerateJwtToken(Guid userId, string firstName, string lastName, string role);
    string HashPassword(string password);
    bool ValidatePassword(string password, string storedPassword);
}
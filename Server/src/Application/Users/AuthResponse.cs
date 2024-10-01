namespace Application.Users;

public record AuthResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
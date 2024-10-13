namespace Application.Users.Login;

public record LoginResponse(
    string FirstName,
    string LastName,
    string Email,
    string Token);
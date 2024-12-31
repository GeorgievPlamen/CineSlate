namespace Application.Users.Login;

public record LoginResponse(
    string Username,
    string Email,
    string Token,
    Guid Id,
    string Bio);
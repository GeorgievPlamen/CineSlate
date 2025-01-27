namespace Application.Users.Login;

public record LoginResponse(
    string Username,
    string Email,
    string Token,
    string RefreshToken,
    Guid Id,
    string Bio,
    string PictureBase64);
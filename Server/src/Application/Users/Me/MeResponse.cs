namespace Application.Users.Me;

public record MeResponse(
    string Username,
    string Email,
    Guid Id,
    string Bio,
    string PictureBase64);
namespace Api.Features.Users.Requests;

public record RegisterRequest(string Username, string Email, string Password);

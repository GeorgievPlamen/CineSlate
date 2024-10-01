namespace Api.User.Requests;

public record RegisterRequest(string FirstName, string LastName, string Email, string Password);

namespace Api.Features.Users.Requests;

public record UpdateUserRequest(Guid Id, string? Bio, string? Picture);
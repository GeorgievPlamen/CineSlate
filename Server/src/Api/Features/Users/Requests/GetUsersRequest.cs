namespace Api.Features.Users.Requests;

public record GetUsersRequest(List<Guid> UserIds);
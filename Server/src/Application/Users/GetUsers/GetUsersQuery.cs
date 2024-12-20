using Application.Common;
using MediatR;

namespace Application.Users.GetUsers;

public record GetUsersQuery(List<Guid> UserIds) : IRequest<Result<List<UserResponse>>>;
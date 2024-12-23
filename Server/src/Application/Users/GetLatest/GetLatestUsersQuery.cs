using Application.Common;
using Application.Users.GetUsers;
using MediatR;

namespace Application.Users.GetLatest;

public record GetLatestUsersQuery(int Page) : IRequest<Result<Paged<UserResponse>>>;
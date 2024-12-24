using Application.Common;
using Application.Users.GetUsers;
using Application.Users.Interfaces;
using MediatR;

namespace Application.Users.GetLatest;

public class GetLatestUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetLatestUsersQuery, Result<Paged<UserResponse>>>
{
    public async Task<Result<Paged<UserResponse>>> Handle(GetLatestUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetPagedAsync(request.Page, Paged.DefaultSize, cancellationToken);

        var result = users.Values.Select(u => u.ToResponse()).ToList();

        return Result<Paged<UserResponse>>.Success(new Paged<UserResponse>(
            result,
            users.CurrentPage,
            users.HasNextPage,
            users.HasPreviousPage,
            users.TotalCount));
    }
}

using Application.Common;
using Application.Users.Interfaces;
using Domain.Users.ValueObjects;
using MediatR;

namespace Application.Users.GetUsers;

public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, Result<List<UserResponse>>>
{
    public async Task<Result<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var userIds = request.UserIds.Select(UserId.Create);
        var result = await userRepository.GetManyByIdAsync(userIds, cancellationToken);

        return Result<List<UserResponse>>.Success([.. result.Select(u => u.ToResponse())]);
    }
}

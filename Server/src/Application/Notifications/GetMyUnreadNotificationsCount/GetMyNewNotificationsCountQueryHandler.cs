using System.Security.Claims;

using Application.Common;
using Application.Notifications.Interfaces;

using Domain.Users.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Application.Notifications.GetMyUnreadNotificationsCount;

public class GetMyNewNotificationsCountQueryHandler(IHttpContextAccessor httpContextAccessor, INotificationRepository notificationRepository) : IRequestHandler<GetMyNewNotificationsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetMyNewNotificationsCountQuery request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        ArgumentException.ThrowIfNullOrWhiteSpace(userIdClaim?.Value);
        var userId = UserId.Create(Guid.Parse(userIdClaim?.Value!));

        var res = await notificationRepository.GetNewCountByUserIdAsync(userId, cancellationToken);

        return Result<int>.Success(res);
    }
}
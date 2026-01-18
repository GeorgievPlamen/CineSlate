using System.Security.Claims;

using Application.Common;
using Application.Notifications.Interfaces;

using Domain.Users.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Application.Notifications.GetMyNotifications;

public class GetMyNotificationsQueryHandler(IHttpContextAccessor httpContextAccessor, INotificationRepository notificationRepository) : IRequestHandler<GetMyNotificationsQuery, Result<Paged<NotificationResponse>>>
{
    public async Task<Result<Paged<NotificationResponse>>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        ArgumentException.ThrowIfNullOrWhiteSpace(userIdClaim?.Value);
        var userId = UserId.Create(Guid.Parse(userIdClaim?.Value!));

        var result = await notificationRepository.GetManyPagedByUserIdAsync(userId, request.Page, 10, cancellationToken);

        return Result<Paged<NotificationResponse>>.Success(new(
            [.. result.Values.Select(x => new NotificationResponse(
                x.Id.Value,
                x.UserId.Value,
                x.Type,
                x.Status,
                x.Data,
                x.CreatedOn))],
            result.CurrentPage,
            result.HasNextPage,
            result.HasPreviousPage,
            result.TotalCount));
    }
}
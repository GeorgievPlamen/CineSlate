using System.Security.Claims;

using Application.Common;
using Application.Notifications.Interfaces;

using Domain.Users.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace Application.Notifications.SetAllSeenByUserId;

public class SetAllSeenByUserIdCommandHandler(IHttpContextAccessor httpContextAccessor, INotificationRepository notificationRepository) : IRequestHandler<SetAllSeenByUserIdCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SetAllSeenByUserIdCommand request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;
        var userIdClaim = httpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        ArgumentException.ThrowIfNullOrWhiteSpace(userIdClaim?.Value);
        var userId = UserId.Create(Guid.Parse(userIdClaim?.Value!));

        var res = await notificationRepository.SetAllSeenByUserIdAsync(userId, cancellationToken);

        return Result<bool>.Success(res);
    }
}
using Api.Common;

using Application.Common;
using Application.Notifications;
using Application.Notifications.GetMyNotifications;

using MediatR;

namespace Api.Features.Notifications;

public static class NotificationsEndpoint
{
    public const string Uri = "api/notifications";

    public static void MapNotifications(this WebApplication app)
    {
        var reviews = app.MapGroup(Uri).RequireAuthorization();

        reviews.MapGet("/", GetMyNotificationsAsync);
    }

    private static async Task<IResult> GetMyNotificationsAsync(int page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<NotificationResponse>>.Match(await mediatr.Send(new GetMyNotificationsQuery(page), cancellationToken));
}
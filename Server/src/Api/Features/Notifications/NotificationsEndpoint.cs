using Api.Common;

using Application.Common;
using Application.Notifications;
using Application.Notifications.GetMyNotifications;
using Application.Notifications.GetMyUnreadNotificationsCount;
using Application.Notifications.SetAllSeenByUserId;

using MediatR;

namespace Api.Features.Notifications;

public static class NotificationsEndpoint
{
    public const string Uri = "api/notifications";

    public static void MapNotifications(this WebApplication app)
    {
        var reviews = app.MapGroup(Uri).RequireAuthorization();

        reviews.MapGet("/", GetMyNotificationsAsync);
        reviews.MapGet("/new-count", GetMyNewNotificationsCountAsync);

        reviews.MapPut("/set-all-seen", SetAllSeenByUserIdAsync);
    }

    private static async Task<IResult> GetMyNotificationsAsync(int page, ISender mediatr, CancellationToken cancellationToken)
        => Response<Paged<NotificationResponse>>.Match(await mediatr.Send(new GetMyNotificationsQuery(page), cancellationToken));

    private static async Task<IResult> GetMyNewNotificationsCountAsync(ISender mediatr, CancellationToken cancellationToken)
        => Response<int>.Match(await mediatr.Send(new GetMyNewNotificationsCountQuery(), cancellationToken));

    private static async Task<IResult> SetAllSeenByUserIdAsync(ISender mediatr, CancellationToken cancellationToken)
        => Response<bool>.Match(await mediatr.Send(new SetAllSeenByUserIdCommand(), cancellationToken));
}
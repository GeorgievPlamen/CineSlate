using Domain.Movies.Reviews.Events;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace Api.Features.Notifications.EventHandlers;

public class PushRealtimeNotificationOnReviewLikedHandler(IHubContext<NotificationHub> hubContext) : INotificationHandler<LikedReviewEvent>
{
    public async Task Handle(LikedReviewEvent notification, CancellationToken cancellationToken)
    {
        var user = hubContext.Clients.User(notification.AuthorId.Value.ToString());

        await user.SendAsync("notify", notification.ToString(), cancellationToken);
    }
}
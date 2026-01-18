using Application.Notifications.Interfaces;

using Domain.Movies.Reviews.Events;
using Domain.Notifications;
using Domain.Notifications.Enums;
using Domain.Notifications.ValueObjects;

using MediatR;

namespace Application.Notifications.EventHandlers;

public class CreateNotificationOnReviewLikedHandler(INotificaitonRepository notificaitonRepository) : INotificationHandler<LikedReviewEvent>
{
    public async Task Handle(LikedReviewEvent notification, CancellationToken cancellationToken)
    {
        var notificationAggregate = NotificationAggregate.Create(
            new NotificationId(Guid.CreateVersion7()),
            NotificationType.LikedReview,
            notification.AuthorId);

        notificationAggregate.Metadata.Add("user-id", notification.UserId.Value.ToString());
        notificationAggregate.Metadata.Add("author-id", notification.AuthorId.Value.ToString());
        notificationAggregate.Metadata.Add("review-id", notification.ReviewId.Value.ToString());
        notificationAggregate.Metadata.Add("movie-id", notification.MovieId.Value.ToString());

        await notificaitonRepository.CreateAsync(notificationAggregate, cancellationToken);
    }
}
using Application.Notifications.Interfaces;

using Domain.Movies.Reviews.Events;
using Domain.Notifications;
using Domain.Notifications.Enums;
using Domain.Notifications.ValueObjects;

using MediatR;

namespace Application.Notifications.EventHandlers;

public class CreateNotificationOnReviewLikedHandler(INotificationRepository notificaitonRepository) : INotificationHandler<LikedReviewEvent>
{
    public async Task Handle(LikedReviewEvent notification, CancellationToken cancellationToken)
    {
        var notificationAggregate = NotificationAggregate.Create(
            new NotificationId(Guid.CreateVersion7()),
            NotificationType.LikedReview,
            notification.AuthorId);

        notificationAggregate.Data.Add("user-id", notification.UserId.Value.ToString());
        notificationAggregate.Data.Add("author-id", notification.AuthorId.Value.ToString());
        notificationAggregate.Data.Add("review-id", notification.ReviewId.Value.ToString());
        notificationAggregate.Data.Add("movie-id", notification.MovieId.Value.ToString());

        await notificaitonRepository.CreateAsync(notificationAggregate, cancellationToken);
    }
}
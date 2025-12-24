using Application.Common.Interfaces;
using Domain.Movies.Reviews.Events;
using MediatR;

namespace Application.Reviews.Likes;

public class LikedReviewHandler(INotifier notifier) : INotificationHandler<LikedReviewEvent>
{
    public async Task Handle(LikedReviewEvent notification, CancellationToken cancellationToken)
    {
        await notifier.NotifyUser(notification.UserId, "notify", "someone liked your review");
    }
}
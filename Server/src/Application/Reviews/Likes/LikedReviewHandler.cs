using Domain.Movies.Reviews.Events;

using MediatR;

namespace Application.Reviews.Likes;

public class LikedReviewHandler : INotificationHandler<LikedReviewEvent>
{
    public Task Handle(LikedReviewEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
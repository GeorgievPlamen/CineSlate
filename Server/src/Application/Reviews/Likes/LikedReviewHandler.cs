using Domain.Movies.Reviews.Events;

using MediatR;

namespace Application.Reviews.Likes;

public class LikedReviewHandler : INotificationHandler<LikedReviewEvent>
{
    public async Task Handle(LikedReviewEvent notification, CancellationToken cancellationToken)
    {
        System.Console.WriteLine("Someone liked a review");
    }
}
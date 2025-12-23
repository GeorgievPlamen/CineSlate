using Domain.Common.Models;

namespace Domain.Movies.Reviews.Events;

public record LikedReviewEvent(Guid UserId, string UserName, Guid AuthorId) : DomainEvent;
using Domain.Common.Models;
using Domain.Movies.Reviews.ValueObjects;
using Domain.Movies.ValueObjects;
using Domain.Users.ValueObjects;

namespace Domain.Movies.Reviews.Events;

public record LikedReviewEvent(UserId UserId, UserId AuthorId, ReviewId ReviewId, MovieId MovieId) : DomainEvent;
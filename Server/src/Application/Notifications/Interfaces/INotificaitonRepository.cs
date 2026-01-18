using Application.Common;

using Domain.Notifications;
using Domain.Notifications.ValueObjects;
using Domain.Users.ValueObjects;

namespace Application.Notifications.Interfaces;

public interface INotificaitonRepository
{
    Task<NotificationAggregate?> GetByIdAsync(NotificationId id, CancellationToken cancellationToken);
    Task<Paged<NotificationAggregate>> GetManyPagedByUserIdAsync(UserId userId, int page, int count, CancellationToken cancellationToken);
    Task<bool> CreateAsync(NotificationAggregate notification, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(NotificationAggregate notification, CancellationToken cancellationToken);
}
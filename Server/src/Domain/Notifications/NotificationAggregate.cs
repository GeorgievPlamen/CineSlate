using Domain.Common.Models;
using Domain.Notifications.Enums;
using Domain.Notifications.ValueObjects;
using Domain.Users.ValueObjects;

namespace Domain.Notifications;

public class NotificationAggregate(NotificationId id) : AggregateRoot<NotificationId>(id)
{
    public NotificationType Type { get; private set; }
    public UserId UserId { get; private set; } = null!;
    public Dictionary<string, string> Metadata { get; private set; } = [];
    public NotificationStatus Status { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }

    public static NotificationAggregate Create(NotificationId id, NotificationType type, UserId userId, DateTimeOffset? createdOn = null) => new(id)
    {
        Type = type,
        UserId = userId,
        Status = NotificationStatus.New,
        CreatedOn = createdOn ?? DateTimeOffset.MinValue
    };

    public void SetSeen() => Status = NotificationStatus.Seen;
}
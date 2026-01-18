using Domain.Notifications;
using Domain.Notifications.Enums;
using Domain.Notifications.ValueObjects;
using Domain.Users.ValueObjects;

using Infrastructure.Database.Models;

namespace Infrastructure.Repositories.MappingExtensions;

public static class NotificationMappings
{
    public static NotificationModel ToModel(this NotificationAggregate notification)
        => new()
        {
            Id = notification.Id.Value,
            UserId = notification.UserId.Value,
            CreatedAt = notification.CreatedOn,
            Data = notification.Data,
            Status = notification.Status,
            Type = notification.Type,
        };

    public static NotificationAggregate Unwrap(this NotificationModel notificationModel)
    {
        var notification = NotificationAggregate.Create(
            new NotificationId(notificationModel.Id),
            notificationModel.Type,
            UserId.Create(notificationModel.UserId));

        if (notificationModel.Status == NotificationStatus.Seen)
            notification.SetSeen();

        foreach (var kvp in notificationModel.Data)
        {
            notification.Data.Add(kvp.Key, kvp.Value);
        }

        return notification;
    }
}
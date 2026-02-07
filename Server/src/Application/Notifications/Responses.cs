using Domain.Notifications.Enums;

namespace Application.Notifications;

public record NotificationResponse(
    Guid Id,
    Guid UserId,
    NotificationType Type,
    NotificationStatus Status,
    Dictionary<string, string> Data,
    DateTimeOffset CreatedOn);
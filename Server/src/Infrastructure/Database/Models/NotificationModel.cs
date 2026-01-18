using Domain.Notifications.Enums;

using Infrastructure.Database.Models.Base;

namespace Infrastructure.Database.Models;

public class NotificationModel : BaseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Dictionary<string, string> Data { get; set; } = [];
    public NotificationType Type { get; set; }
    public NotificationStatus Status { get; set; }
}
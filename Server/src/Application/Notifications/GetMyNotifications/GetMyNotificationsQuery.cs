using Application.Common;

using MediatR;

namespace Application.Notifications.GetMyNotifications;

public record GetMyNotificationsQuery(int Page) : IRequest<Result<Paged<NotificationResponse>>>;
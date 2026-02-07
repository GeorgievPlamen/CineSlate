using Application.Common;

using MediatR;

namespace Application.Notifications.GetMyNotifications;

public record GetMyNotificationsQuery(int Page, int Quantity) : IRequest<Result<Paged<NotificationResponse>>>;
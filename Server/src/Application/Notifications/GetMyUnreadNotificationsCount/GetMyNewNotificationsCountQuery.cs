using Application.Common;

using MediatR;

namespace Application.Notifications.GetMyUnreadNotificationsCount;

public record GetMyNewNotificationsCountQuery : IRequest<Result<int>>;
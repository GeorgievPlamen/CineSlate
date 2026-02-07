using Application.Common;

using MediatR;

namespace Application.Notifications.SetAllSeenByUserId;

public record SetAllSeenByUserIdCommand : IRequest<Result<bool>>;
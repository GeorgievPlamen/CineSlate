using Application.Common.Interfaces;

using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifier;

public class SignalRNotifier(IHubContext<NotificationHub> hubContext) : INotifier
{

    public async Task NotifyUser(Guid userId, string topic, object args)
    {
        await hubContext.Clients.All.SendAsync(topic, args);
    }
}
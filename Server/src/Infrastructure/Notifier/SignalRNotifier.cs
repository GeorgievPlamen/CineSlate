using Application.Common.Interfaces;

using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifier;

public class SignalRNotifier(IHubContext<NotificationHub> hubContext) : INotifier
{

    public async Task NotifyUser(Guid userId, string topic, object args)
    {
        var user = hubContext.Clients.User(userId.ToString());

        await user.SendAsync(topic, args);
        // await hubContext.Clients.All.SendAsync(topic, args);
    }
}
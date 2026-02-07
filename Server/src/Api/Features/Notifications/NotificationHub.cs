using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.Features.Notifications;

[Authorize]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"\nNew connection:  \n\tConnectionId: '{Context.ConnectionId}'\n\tUserId: '{Context.UserIdentifier}'");
    }
}
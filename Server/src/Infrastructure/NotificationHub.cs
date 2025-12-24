using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure;

[Authorize]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"\nNew connection:  \n\tConnectionId: '{Context.ConnectionId}'\n\tUserId: '{Context.UserIdentifier}'");

        await Clients.Caller.SendAsync("notify", "Hello from SignalR");
    }
}
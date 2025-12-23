using Microsoft.AspNetCore.SignalR;

namespace Api.Features.Notifications;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"New connection: {Clients.Caller}");

        await Clients.Caller.SendAsync("Hi from SignalR");
    }
}
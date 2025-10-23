using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public async Task JoinGroup(string role)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{role}"); 
        Console.WriteLine($"👥 User joined Group_{role}");
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var userId = httpContext?.Session.GetInt32("UserId");
        var role = httpContext?.Session.GetInt32("Role");

        if (role != null)
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{role}");

        await base.OnConnectedAsync();
    }
}

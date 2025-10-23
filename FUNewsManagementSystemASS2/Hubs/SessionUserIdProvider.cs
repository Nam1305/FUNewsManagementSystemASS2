using Microsoft.AspNetCore.SignalR;

namespace FUNewsManagementSystem.WebApp.Hubs
{
    public class SessionUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var httpContext = connection.GetHttpContext();
            var userId = httpContext?.Session.GetInt32("UserId");
            return userId?.ToString();
        }
    }
}

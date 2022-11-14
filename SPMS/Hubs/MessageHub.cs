using Microsoft.AspNetCore.SignalR;

namespace CPMS.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            if (string.IsNullOrEmpty(user))
                await Clients.All.SendAsync("ReceiveMessage", message);
            else
                await Clients.User(user).SendAsync("ReceiveMessage", message);
        }
    }
}
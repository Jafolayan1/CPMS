using Microsoft.AspNetCore.SignalR;

namespace SPMS.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await base.OnDisconnectedAsync(exception);
		}
	}
}
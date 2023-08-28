using Microsoft.AspNetCore.SignalR;

namespace NotificationService;

public class NotificationHub : Hub, INotificationHub
{
	public async Task SendToAllAsync(string message)
		=> await Clients.All.SendAsync("ReceiveMessage", message);

	public async Task SendToUserAsync(string userId, string message)
    => await Clients.User(userId).SendAsync("ReceiveMessage", message);
}

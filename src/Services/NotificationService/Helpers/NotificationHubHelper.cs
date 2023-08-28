using Microsoft.AspNetCore.SignalR;

namespace NotificationService.Helpers;

public class NotificationHubHelper(IHubContext<NotificationHub> context) : INotificationHubHelper
{
	public Task SendToAllAsync(string user, string message)
		=> context.Clients.All.SendAsync("ReceiveMessage", user, message);
	
	public Task SendToUserAsync(string userId, string message)
		=> context.Clients.User(userId).SendAsync("ReceiveMessage", "Server", message);

	public async Task SendToGroupAsync(string groupId, string message)
		=> await context.Clients.Group(groupId).SendAsync("ReceiveMessage", "Server", message);
}

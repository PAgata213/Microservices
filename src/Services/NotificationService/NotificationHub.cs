using Microsoft.AspNetCore.SignalR;

namespace NotificationService;

public class NotificationHub : Hub
{
	public async Task SendToAllAsync(string user, string message)
		=> await Clients.All.SendAsync("ReceiveMessage", user, message);

	public async Task SendToUserAsync(string userId, string message)
		=> await Clients.Group(userId).SendAsync("ReceiveMessage", "Server", message);

	public override async Task OnConnectedAsync()
	{
		var guid = Guid.NewGuid();
		await Groups.AddToGroupAsync(Context.ConnectionId, guid.ToString());
		await SendToUserAsync(guid.ToString(), $"Connected. Your user Id: {guid}");
	}
}

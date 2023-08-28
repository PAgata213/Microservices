namespace NotificationService;

public interface INotificationHub
{
	Task SendToAllAsync(string message);
	Task SendToUserAsync(string userId, string message);
}
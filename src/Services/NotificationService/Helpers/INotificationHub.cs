namespace NotificationService.Helpers;

public interface INotificationHubHelper
{
	Task SendToAllAsync(string user, string message);
	Task SendToUserAsync(string userId, string message);
	Task SendToGroupAsync(string groupId, string message);
}
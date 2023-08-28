using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Events;
using NotificationService.Helpers;

namespace NotificationService.Handlers;

public class ReservationFailedConsumer(INotificationHubHelper notificationHubHelper) : IConsumer<ReservationFailed>
{
	public async Task Consume(ConsumeContext<ReservationFailed> context)
		=> await notificationHubHelper.SendToGroupAsync(context.Message.UserId.ToString(), context.Message.ErrorMessage ?? "Reservation failed");
}

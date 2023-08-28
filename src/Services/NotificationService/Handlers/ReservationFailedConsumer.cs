using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Events;

namespace NotificationService.Handlers;

public class ReservationFailedConsumer(INotificationHub notificationHub) : IConsumer<ReservationFailed>
{
	public async Task Consume(ConsumeContext<ReservationFailed> context)
		=> await notificationHub.SendToUserAsync(context.Message.UserId.ToString(), context.Message.ErrorMessage ?? "Reservation failed");
}

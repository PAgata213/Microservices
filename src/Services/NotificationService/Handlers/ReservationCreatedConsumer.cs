using MassTransit;

using Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Events;
using NotificationService.Helpers;

namespace NotificationService.Handlers;

public class ReservationCreatedConsumer(INotificationHubHelper notificationHubHelper) : IConsumer<ReservationCreated>
{
	public Task Consume(ConsumeContext<ReservationCreated> context)
		=> notificationHubHelper.SendToGroupAsync(context.Message.UserId.ToString(), $"Reservation created, ID: {context.Message.CorrelationId}, Fly: {context.Message.FlyReservationId}, Hotel: {context.Message.HotelReservationId}, Car: {context.Message.CarReservationId}");
}

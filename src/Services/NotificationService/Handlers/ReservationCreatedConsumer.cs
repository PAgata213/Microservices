using MassTransit;

using Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Events;

namespace NotificationService.Handlers;

public class ReservationCreatedConsumer(INotificationHub notificationHub) : IConsumer<ReservationCreated>
{
	public Task Consume(ConsumeContext<ReservationCreated> context)
		=> notificationHub.SendToUserAsync(context.Message.UserId.ToString(), $"Reservation created, ID: {context.Message.CorrelationId}, Fly: {context.Message.FlyReservationId}, Hotel: {context.Message.HotelReservationId}, Car: {context.Message.CarReservationId}");
}

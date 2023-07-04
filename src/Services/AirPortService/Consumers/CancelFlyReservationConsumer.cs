using AirPortService.Commands;

using MediatR;
using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Commands;
using Microservices.Gateway.Shared.ConsumerSaga.Events;

internal class CancelFlyReservationConsumer(IMediator mediator) : IConsumer<CancelFlyReservation>
{
  public async Task Consume(ConsumeContext<CancelFlyReservation> context)
  {
    await mediator.Send(new CancelFlyReservationCommand
    {
      ReservationId = context.Message.ReservationId
    });

    await context.Publish(new FlyReservationCanceled
    {
      CorrelationId = context.Message.CorrelationId,
      ReservationId = context.Message.ReservationId
    });
  }
}
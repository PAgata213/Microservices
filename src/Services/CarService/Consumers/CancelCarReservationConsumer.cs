using CarService.Commands;

using MediatR;
using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Commands;
using Microservices.Gateway.Shared.ConsumerSaga.Events;

internal class CancelCarReservationConsumer(IMediator mediator) : IConsumer<CancelCarReservation>
{
  public async Task Consume(ConsumeContext<CancelCarReservation> context)
  {
    await mediator.Send(new CancelCarReservationCommand
    {
      ReservationId = context.Message.ReservationId
    });

    await context.Publish(new CarReservationCanceled
    {
      CorrelationId = context.Message.CorrelationId,
      ReservationId = context.Message.ReservationId
    });
  }
}
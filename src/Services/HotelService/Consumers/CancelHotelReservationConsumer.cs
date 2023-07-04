using HotelService.Commands;

using MediatR;
using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Commands;
using Microservices.Gateway.Shared.ConsumerSaga.Events;

internal class CancelHotelReservationConsumer(IMediator mediator) : IConsumer<CancelHotelReservation>
{
  public async Task Consume(ConsumeContext<CancelHotelReservation> context)
  {
    await mediator.Send(new CancelHotelReservationCommand
    {
      ReservationId = context.Message.ReservationId
    });

    await context.Publish(new HotelReservationCanceled
    {
      CorrelationId = context.Message.CorrelationId,
      ReservationId = context.Message.ReservationId
    });
  }
}
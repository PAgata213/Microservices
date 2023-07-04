using HotelService.Commands;

using MediatR;
using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Commands;
using Microservices.Gateway.Shared.ConsumerSaga.Events;

internal class CreateHotelReservationConsumer(IMediator mediator) : IConsumer<CreateHotelReservation>
{
  public async Task Consume(ConsumeContext<CreateHotelReservation> context)
  {
    try
    {
      var result = await mediator.Send(new CreateHotelReservationCommand
      {
        HotelId = context.Message.HotelId,
        UserId = context.Message.UserId
      });

      await context.Publish(new HotelReservationCreated
      {
        CorrelationId = context.Message.CorrelationId,
        ReservationId = result!.Value
      });
    }
    catch(Exception ex)
    {
      await context.Publish(new HotelReservationCreationFailed
      {
        CorrelationId = context.Message.CorrelationId,
        Message = ex.Message
      });
    }
  }
}

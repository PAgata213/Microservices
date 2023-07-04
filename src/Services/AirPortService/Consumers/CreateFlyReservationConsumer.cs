using AirPortService.Commands;

using MediatR;
using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Commands;
using Microservices.Gateway.Shared.ConsumerSaga.Events;

internal class CreateFlyReservationConsumer(IMediator mediator) : IConsumer<CreateFlyReservation>
{
  public async Task Consume(ConsumeContext<CreateFlyReservation> context)
  {
    try
    {
      var result = await mediator.Send(new CreateFlyReservationCommand
      {
        FlyId = context.Message.FlyId,
        UserId = context.Message.UserId,
        SeatNumber = context.Message.SeatNumber,
      });

      await context.Publish(new FlyReservationCreated
      {
        CorrelationId = context.Message.CorrelationId,
        ReservationId = result!.Value
      });
    }
    catch(Exception ex)
    {
      await context.Publish(new FlyReservationCreationFailed
      {
        CorrelationId = context.Message.CorrelationId,
        Message = ex.Message
      });
    }
  }
}

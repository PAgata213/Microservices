using CarService.Commands;

using MediatR;
using MassTransit;

using Microservices.Gateway.Shared.ConsumerSaga.Commands;
using Microservices.Gateway.Shared.ConsumerSaga.Events;

internal class CreateCarReservationConsumer(IMediator mediator) : IConsumer<CreateCarReservation>
{
  public async Task Consume(ConsumeContext<CreateCarReservation> context)
  {
    try
    {
      var result = await mediator.Send(new CreateCarReservationCommand
      {
        CarId = context.Message.CarId,
        UserId = context.Message.UserId
      });

      await context.Publish(new CarReservationCreated
      {
        CorrelationId = context.Message.CorrelationId,
        ReservationId = result!.Value
      });
    }
    catch(Exception ex)
    {
      await context.Publish(new CarReservationCreationFailed
      {
        CorrelationId = context.Message.CorrelationId,
        Message = ex.Message
      });
    }
  }
}
using MassTransit;

using MediatR;

using Microservices.Gateway.Server.Sagas.SagaCommands;
using Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands;

namespace Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga;

public class CreateReservationWithConsumerSagaCommandHandler(IPublishEndpoint publishEndpoint) : IRequestHandler<CreateReservationWithConsumerSagaCommand>
{
  public async Task Handle(CreateReservationWithConsumerSagaCommand request, CancellationToken cancellationToken)
    => await publishEndpoint!.Publish(new StartReservationConsumerSaga
    {
      CorrelationId = NewId.NextGuid(),
      UserId = request.UserId,
      FlyId = request.FlyId,
      SeatNumber = request.SeatNumber,
      HotelId = request.HotelId,
      CarId = request.CarId
    }, cancellationToken);
}

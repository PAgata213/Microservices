using MassTransit;

namespace Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands;

public class StartReservationConsumerSaga : CorrelatedBy<Guid>
{
  public Guid CorrelationId { get; set; }

  public required Guid UserId { get; init; }
  public required Guid FlyId { get; init; }
  public required int SeatNumber { get; init; }
  public required Guid CarId { get; init; }
  public required Guid HotelId { get; init; }
}

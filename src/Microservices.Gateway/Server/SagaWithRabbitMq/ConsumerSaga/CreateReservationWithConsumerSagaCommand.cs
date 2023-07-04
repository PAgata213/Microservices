using MediatR;

namespace Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga;

public record CreateReservationWithConsumerSagaCommand : IRequest
{
  public required Guid UserId { get; init; }
  public required Guid FlyId { get; init; }
  public required int SeatNumber { get; init; }
  public required Guid CarId { get; init; }
  public required Guid HotelId { get; init; }
}

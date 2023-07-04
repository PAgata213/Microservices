using MassTransit;

namespace Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Events;

public record ReservationCreated : CorrelatedBy<Guid>
{
  public Guid CorrelationId { get; set; }
  public Guid FlyReservationId { get; set; }
  public Guid HotelReservationId { get; set; }
  public Guid CarReservationId { get; set; }
}

using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Events;

public class CarReservationCanceled : CorrelatedBy<Guid>
{
  public Guid CorrelationId { get; set; }
  public Guid ReservationId { get; set; }
}
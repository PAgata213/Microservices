using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Events;
public record FlyReservationCreationFailed : CorrelatedBy<Guid>
{
  public Guid CorrelationId { get; set; }
  public string? Message { get; set; }
}
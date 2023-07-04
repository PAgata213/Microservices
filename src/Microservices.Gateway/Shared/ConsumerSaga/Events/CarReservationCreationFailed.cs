using MassTransit;
namespace Microservices.Gateway.Shared.ConsumerSaga.Events;
public record CarReservationCreationFailed : CorrelatedBy<Guid>
{
  public Guid CorrelationId { get; set; }
  public string? Message { get; set; }
}
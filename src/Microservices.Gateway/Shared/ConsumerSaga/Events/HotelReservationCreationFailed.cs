using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Events;
public record HotelReservationCreationFailed : CorrelatedBy<Guid>
{
  public Guid CorrelationId { get; set; }
  public string? Message { get; set; }
}

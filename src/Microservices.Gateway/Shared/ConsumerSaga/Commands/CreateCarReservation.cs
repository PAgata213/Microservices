using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Commands;

public record CreateCarReservation : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }

    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
}
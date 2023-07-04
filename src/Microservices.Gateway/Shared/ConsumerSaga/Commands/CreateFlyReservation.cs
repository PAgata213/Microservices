using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Commands;

public record CreateFlyReservation : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }

    public Guid UserId { get; set; }
    public Guid FlyId { get; set; }
    public int SeatNumber { get; set; }
}

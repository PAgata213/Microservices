using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Commands;

public record CancelHotelReservation : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public Guid ReservationId { get; set; }
}

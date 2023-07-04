using MassTransit;

namespace Microservices.Gateway.Shared.ConsumerSaga.Commands;

public record CreateHotelReservation : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }

    public Guid UserId { get; set; }
    public Guid HotelId { get; set; }
}
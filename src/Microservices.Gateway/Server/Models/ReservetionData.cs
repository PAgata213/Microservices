namespace Microservices.Gateway.Server.Models;

public record ReservetionData
{
    public Guid Id { get; set; }
    public Guid FlyReservationId { get; set; }
    public Guid HotelReservationId { get; set; }
    public Guid CarReservationId { get; set; }
}

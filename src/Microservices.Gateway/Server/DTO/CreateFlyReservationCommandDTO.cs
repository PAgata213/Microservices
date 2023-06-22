namespace Microservices.Gateway.Server.DTO;

public record CreateFlyReservationCommandDTO
{
    public Guid FlyId { get; set; }
    public Guid UserId { get; set; }
    public int SeatNumber { get; set; }
}

namespace Microservices.Gateway.Server.DTO;

public class CreateCarReservationCommandDTO
{
    public Guid CarId { get; set; }
    public Guid UserId { get; set; }
}
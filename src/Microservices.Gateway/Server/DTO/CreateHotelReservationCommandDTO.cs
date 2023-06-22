namespace Microservices.Gateway.Server.DTO;

public class CreateHotelReservationCommandDTO
{
    public Guid HotelId { get; set; }
    public Guid UserId { get; set; }
}
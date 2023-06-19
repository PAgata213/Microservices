namespace HotelService.Models;

public record Reservation
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid UserId { get; set; }
  public Guid HotelId { get; set; }
  public bool Confirmed { get; set; }
}

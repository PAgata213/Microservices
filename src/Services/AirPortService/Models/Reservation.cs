namespace AirPortService.Models;

public record Reservation
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid UserId { get; set; }
  public Guid FlyId { get; set; }
  public int SeatNumber { get; set; }
  public bool Confirmed { get; set; }
}

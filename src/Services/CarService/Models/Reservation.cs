namespace CarService.Models;

public record Reservation
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public Guid UserId { get; set; }
  public Guid CarId { get; set; }
  public bool Confirmed { get; set; }
}

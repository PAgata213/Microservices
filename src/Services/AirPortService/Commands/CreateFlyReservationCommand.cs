using MediatR;

namespace AirPortService.Commands;

public class CreateFlyReservationCommand : IRequest<Guid?>
{
  public Guid Id { get; set; }
  public Guid FlyId { get; set; }
  public Guid UserId { get; set; }
  public int SeatNumber { get; set; }
}

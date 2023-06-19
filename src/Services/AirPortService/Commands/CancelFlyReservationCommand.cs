using MediatR;

namespace AirPortService.Commands;

public class CancelFlyReservationCommand : IRequest
{
  public Guid ReservationId { get; set; }
}

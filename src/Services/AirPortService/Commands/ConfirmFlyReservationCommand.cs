using MediatR;

namespace AirPortService.Commands;
public class ConfirmFlyReservationCommand : IRequest
{
  public Guid ReservationId { get; set; }
}
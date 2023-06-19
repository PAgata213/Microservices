using MediatR;

namespace CarService.Commands;
public class ConfirmCarReservationCommand : IRequest
{
  public Guid ReservationId { get; set; }
}
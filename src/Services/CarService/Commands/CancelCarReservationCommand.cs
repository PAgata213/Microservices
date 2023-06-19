using MediatR;

namespace CarService.Commands;

public class CancelCarReservationCommand : IRequest
{
  public Guid ReservationId { get; set; }
}

using MediatR;

namespace HotelService.Commands;

public class CancelHotelReservationCommand : IRequest
{
  public Guid ReservationId { get; set; }
}

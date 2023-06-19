using MediatR;

namespace HotelService.Commands;
public class ConfirmHotelReservationCommand : IRequest
{
  public Guid ReservationId { get; set; }
}
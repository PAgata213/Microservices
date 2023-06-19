using MediatR;

namespace HotelService.Commands;

public class CreateHotelReservationCommand : IRequest<Guid?>
{
  public Guid Id { get; set; }
  public Guid HotelId { get; set; }
  public Guid UserId { get; set; }
}

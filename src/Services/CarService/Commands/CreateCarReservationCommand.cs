using MediatR;

namespace CarService.Commands;

public class CreateCarReservationCommand : IRequest<Guid?>
{
  public Guid Id { get; set; }
  public Guid CarId { get; set; }
  public Guid UserId { get; set; }
}

using CarService.Commands;
using CarService.Models;
using CarService.Repository;

using MediatR;

namespace CarService.Handlers;

public class CreateCarReservationCommandHandler(ICarRepository airPortRepository) : IRequestHandler<CreateCarReservationCommand, Guid?>
{
  public async Task<Guid?> Handle(CreateCarReservationCommand request, CancellationToken cancellationToken)
  {
    if(await airPortRepository.ExistsAsync(x => x.CarId == request.CarId))
    {
      return null;
    }

    var reservation = new Reservation
    {
      CarId = request.CarId,
      UserId = request.UserId
    };

    await airPortRepository.AddAsync(reservation);
    return reservation.Id;
  }
}
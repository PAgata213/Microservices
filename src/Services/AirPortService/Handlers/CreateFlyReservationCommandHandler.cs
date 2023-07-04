using AirPortService.Commands;
using AirPortService.Exceptions;
using AirPortService.Models;
using AirPortService.Repository;

using MediatR;

namespace AirPortService.Handlers;

public class CreateFlyReservationCommandHandler(IAirPortRepository airPortRepository) : IRequestHandler<CreateFlyReservationCommand, Guid?>
{
  public async Task<Guid?> Handle(CreateFlyReservationCommand request, CancellationToken cancellationToken)
  {
    if(await airPortRepository.ExistsAsync(x => x.FlyId == request.FlyId && x.SeatNumber == request.SeatNumber))
    {
      throw new ReservationForGivenFlyAndSeatAlreadyExists(request.FlyId, request.SeatNumber);
    }

    var reservation = new Reservation
    {
      FlyId = request.FlyId,
      UserId = request.UserId,
      SeatNumber = request.SeatNumber
    };

    await airPortRepository.AddAsync(reservation);
    return reservation.Id;
  }
}
using HotelService.Commands;
using HotelService.Models;
using HotelService.Repository;

using MediatR;

namespace HotelService.Handlers;

public class CreateHotelReservationCommandHandler(IHotelRepository HotelRepository) : IRequestHandler<CreateHotelReservationCommand, Guid?>
{
  public async Task<Guid?> Handle(CreateHotelReservationCommand request, CancellationToken cancellationToken)
  {
    if(await HotelRepository.ExistsAsync(x => x.HotelId == request.HotelId))
    {
      return null;
    }

    var reservation = new Reservation
    {
      HotelId = request.HotelId,
      UserId = request.UserId
    };

    await HotelRepository.AddAsync(reservation);
    return reservation.Id;
  }
}
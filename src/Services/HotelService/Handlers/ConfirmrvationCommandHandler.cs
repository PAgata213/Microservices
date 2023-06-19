using HotelService.Commands;
using HotelService.Repository;

using MediatR;

namespace HotelService.Handlers;

public class ConfirmHotelReservationCommandHandler(IHotelRepository HotelRepository) : IRequestHandler<ConfirmHotelReservationCommand>
{
  public async Task Handle(ConfirmHotelReservationCommand request, CancellationToken cancellationToken)
  {
    var reservation = await HotelRepository.GetAsync(request.ReservationId);

    if(reservation is null)
    {
      throw new KeyNotFoundException($"Reservation with id {request.ReservationId} not found");
    }

    reservation.Confirmed = true;

    await HotelRepository.SaveChangesAsync();
  }
}

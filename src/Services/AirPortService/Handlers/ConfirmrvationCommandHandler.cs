using AirPortService.Commands;
using AirPortService.Repository;

using MediatR;

namespace AirPortService.Handlers;

public class ConfirmFlyReservationCommandHandler(IAirPortRepository airPortRepository) : IRequestHandler<ConfirmFlyReservationCommand>
{
  public async Task Handle(ConfirmFlyReservationCommand request, CancellationToken cancellationToken)
  {
    var reservation = await airPortRepository.GetAsync(request.ReservationId);

    if(reservation is null)
    {
      throw new KeyNotFoundException($"Reservation with id {request.ReservationId} not found");
    }

    reservation.Confirmed = true;

    await airPortRepository.SaveChangesAsync();
  }
}

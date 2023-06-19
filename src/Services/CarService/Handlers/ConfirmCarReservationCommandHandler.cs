using CarService.Commands;
using CarService.Repository;

using MediatR;

namespace CarService.Handlers;

public class ConfirmCarReservationCommandHandler(ICarRepository airPortRepository) : IRequestHandler<ConfirmCarReservationCommand>
{
  public async Task Handle(ConfirmCarReservationCommand request, CancellationToken cancellationToken)
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

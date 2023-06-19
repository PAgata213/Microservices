using CarService.Commands;
using CarService.Repository;

using MediatR;

namespace CarService.Handlers;

public class CancelCarReservationCommandHandler(ICarRepository airPortRepository) : IRequestHandler<CancelCarReservationCommand>
{
  public async Task Handle(CancelCarReservationCommand request, CancellationToken cancellationToken)
    => await airPortRepository.RemoveAsync(request.ReservationId);
}

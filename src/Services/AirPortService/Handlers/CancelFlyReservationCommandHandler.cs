using AirPortService.Commands;
using AirPortService.Repository;

using MediatR;

namespace AirPortService.Handlers;

public class CancelFlyReservationCommandHandler(IAirPortRepository airPortRepository) : IRequestHandler<CancelFlyReservationCommand>
{
  public async Task Handle(CancelFlyReservationCommand request, CancellationToken cancellationToken)
    => await airPortRepository.RemoveAsync(request.ReservationId);
}

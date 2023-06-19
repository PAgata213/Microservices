using HotelService.Commands;
using HotelService.Repository;

using MediatR;

namespace HotelService.Handlers;

public class CancelHotelReservationCommandHandler(IHotelRepository HotelRepository) : IRequestHandler<CancelHotelReservationCommand>
{
  public async Task Handle(CancelHotelReservationCommand request, CancellationToken cancellationToken)
    => await HotelRepository.RemoveAsync(request.ReservationId);
}

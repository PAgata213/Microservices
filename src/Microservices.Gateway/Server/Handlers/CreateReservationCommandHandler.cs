using MediatR;

using Microservices.Gateway.Server.Commands;
using Microservices.Gateway.Server.Services;

namespace Microservices.Gateway.Server.Handlers;

public class CreateReservationCommandHandler(IReservationService reservationService) : IRequestHandler<CreateReservationCommand, Guid?>
{
  public async Task<Guid?> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
  {
    //Create fly reservation
    
    var flyReservationData = await reservationService.CreateFlyReservation(request.UserId, request.FlyId, request.SeatNumber);
    if(!flyReservationData.Created)
    {
      throw new Exception("Error creating fly reservation");
    }

    //Create Hotel reservation
    var hotelReservationData = await reservationService.CreateHotelReservation(request.UserId, request.HotelId);
    if(!hotelReservationData.Created)
    {
      //Cancel fly reservation
      await reservationService.CancelFlyReservation(flyReservationData.ReservationId);
      throw new Exception("Error creating hotel reservation");
    }

    //Create Hotel reservation
    var carReservationData = await reservationService.CreateCarReservation(request.UserId, request.CarId);
    if(!carReservationData.Created)
    {
      //Cancel fly reservation
      await reservationService.CancelFlyReservation(flyReservationData.ReservationId);
      await reservationService.CancelHotelReservation(hotelReservationData.ReservationId);
      throw new Exception("Error creating car reservation");
    }

    return Guid.NewGuid();
  }
}
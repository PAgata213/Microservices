using Microservices.Gateway.Server.Models;

namespace Microservices.Gateway.Server.Services;

public interface IReservationService
{
  Task<FlyReservationData> CreateFlyReservation(Guid userId, Guid flyId, int SeatNumber);
  Task<bool> CancelFlyReservation(Guid FlyReservationId);
  Task<HotelReservationData> CreateHotelReservation(Guid userId, Guid hotelId);
  Task<bool> CancelHotelReservation(Guid FlyReservationId);
  Task<CarReservationData> CreateCarReservation(Guid userId, Guid carId);
}
using Microservices.Gateway.Server.DTO;
using Microservices.Gateway.Server.Helpers;
using Microservices.Gateway.Server.Models;

namespace Microservices.Gateway.Server.Services;

internal sealed class ReservationService(IHttpClientHelper httpClientHelper) : IReservationService
{
  public async Task<FlyReservationData> CreateFlyReservation(Guid userId, Guid flyId, int SeatNumber)
  {
    var createFlyReservationResponse = await httpClientHelper.PostAsync<CreateFlyReservationCommandDTO, Guid>($"http://AirPortService-node:5100/CreateFlyReservation", new CreateFlyReservationCommandDTO 
    {
      UserId = userId,
      FlyId = flyId,
      SeatNumber = SeatNumber
    });
    return new(createFlyReservationResponse.Data, createFlyReservationResponse.IsSuccess);
  }

  public async Task<bool> CancelFlyReservation(Guid FlyReservationId)
    => await httpClientHelper.PostAsync<CreateFlyReservationCommandDTO, Guid>($"http://AirPortService-node:5100/CancelFlyReservation/{FlyReservationId}");

  public async Task<HotelReservationData> CreateHotelReservation(Guid userId, Guid hotelId)
  {
    var createHotelReservationResponse = await httpClientHelper.PostAsync<CreateHotelReservationCommandDTO, Guid>($"http://HotelService-node:5200/CreateHotelReservation", new CreateHotelReservationCommandDTO 
    {
      UserId = userId,
      HotelId = hotelId,
    });
    return new(createHotelReservationResponse.Data, createHotelReservationResponse.IsSuccess);
  }

  public async Task<bool> CancelHotelReservation(Guid FlyReservationId)
    => await httpClientHelper.PostAsync<CreateFlyReservationCommandDTO, Guid>($"http://HotelService-node:5200/CancelHotelReservation/{FlyReservationId}");

  public async Task<CarReservationData> CreateCarReservation(Guid userId, Guid carId)
  {
    var createCarReservationResponse = await httpClientHelper.PostAsync<CreateCarReservationCommandDTO, Guid>($"http://CarService-node:5300/CreateCarReservation", new CreateCarReservationCommandDTO 
    {
      UserId = userId,
      CarId = carId,
    });
    return new(createCarReservationResponse.Data, createCarReservationResponse.IsSuccess);
  }
}

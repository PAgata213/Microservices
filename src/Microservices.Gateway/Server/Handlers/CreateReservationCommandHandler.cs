using MediatR;

using Microservices.Gateway.Server.Commands;
using Microservices.Gateway.Server.DTO;
using Microservices.Gateway.Server.Helpers;

namespace Microservices.Gateway.Server.Handlers;

public class CreateReservationCommandHandler(IHttpClientHelper httpClientHelper) : IRequestHandler<CreateReservationCommand, Guid?>
{
  public async Task<Guid?> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
  {
    //Create fly reservation
    var flyReservationData = await CreateFlyReservation(request);
    if(!flyReservationData.Created)
    {
      throw new Exception("Error creating fly reservation");
    }

    //Create Hotel reservation
    var hotelReservationData = await CreateHotelReservation(request);
    if(!hotelReservationData.Created)
    {
      //Cancel fly reservation
      await CancelFlyReservation(flyReservationData.ReservationId);
      throw new Exception("Error creating hotel reservation");
    }

    //Create Hotel reservation
    var carReservationData = await CreateCarReservation(request);
    if(!carReservationData.Created)
    {
      //Cancel fly reservation
      await CancelFlyReservation(flyReservationData.ReservationId);
      await CancelHotelReservation(hotelReservationData.ReservationId);
      throw new Exception("Error creating car reservation");
    }

    return Guid.NewGuid();
  }

  private async Task<FlyReservationData> CreateFlyReservation(CreateReservationCommand request)
  {
    var createFlyReservationResponse = await httpClientHelper.PostAsync<CreateFlyReservationCommandDTO, Guid>($"http://AirPortService-node:5100/CreateFlyReservation", new CreateFlyReservationCommandDTO 
    {
      UserId = request.UserId,
      FlyId = request.FlyId,
      SeatNumber = request.SeatNumber
    });
    return new(createFlyReservationResponse.Data, createFlyReservationResponse.IsSuccess);
  }

  private async Task<bool> CancelFlyReservation(Guid FlyReservationId)
    => await httpClientHelper.PostAsync<CreateFlyReservationCommandDTO, Guid>($"http://AirPortService-node:5100/CancelFlyReservation/{FlyReservationId}");

  private async Task<HotelReservationData> CreateHotelReservation(CreateReservationCommand request)
  {
    var createHotelReservationResponse = await httpClientHelper.PostAsync<CreateHotelReservationCommandDTO, Guid>($"http://HotelService-node:5200/CreateHotelReservation", new CreateHotelReservationCommandDTO 
    {
      UserId = request.UserId,
      HotelId = request.HotelId,
    });
    return new(createHotelReservationResponse.Data, createHotelReservationResponse.IsSuccess);
  }

  private async Task<bool> CancelHotelReservation(Guid FlyReservationId)
    => await httpClientHelper.PostAsync<CreateFlyReservationCommandDTO, Guid>($"http://HotelService-node:5200/CancelHotelReservation/{FlyReservationId}");

  private async Task<CarReservationData> CreateCarReservation(CreateReservationCommand request)
  {
    var createCarReservationResponse = await httpClientHelper.PostAsync<CreateCarReservationCommandDTO, Guid>($"http://CarService-node:5300/CreateCarReservation", new CreateCarReservationCommandDTO 
    {
      UserId = request.UserId,
      CarId = request.CarId,
    });
    return new(createCarReservationResponse.Data, createCarReservationResponse.IsSuccess);
  }
}

internal record FlyReservationData(Guid ReservationId, bool Created);
internal record HotelReservationData(Guid ReservationId, bool Created);
internal record CarReservationData(Guid ReservationId, bool Created);

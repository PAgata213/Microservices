using Chronicle;

using Microservices.Gateway.Server.Sagas.SagaCommands;
using Microservices.Gateway.Server.Services;

namespace Microservices.Gateway.Server.Sagas;

internal class CreateReservationSaga(IReservationService reservationService) : Saga<CreateReservationSagaData>,
  ISagaStartAction<CreateFlyReservation>,
  ISagaAction<CreateHotelReservation>,
  ISagaAction<CreateCarReservation>,
  ISagaAction<CompleteReservationSaga>
{
  public async Task HandleAsync(CreateFlyReservation message, ISagaContext context)
  {
    var result = await reservationService.CreateFlyReservation(message.UserId, message.FlyId, message.SeatNumber);
    if(!result.Created)
    {
      await RejectAsync(new Exception("Error creating fly reservation"));
    }
    Data.FlyReservationId = result.ReservationId;
  }
  public async Task CompensateAsync(CreateFlyReservation message, ISagaContext context)
  {
    if(Data.FlyReservationId != default)
    {
      await reservationService.CancelFlyReservation(Data.FlyReservationId);
    }
  }

  public async Task HandleAsync(CreateHotelReservation message, ISagaContext context)
  {
    var result = await reservationService.CreateHotelReservation(message.UserId, message.HotelId);
    if(!result.Created)
    {
      await RejectAsync(new Exception("Error creating hotel reservation"));
    }
    Data.HotelReservationId = result.ReservationId;
  }
  public async Task CompensateAsync(CreateHotelReservation message, ISagaContext context)
  {
    if(Data.HotelReservationId != default)
    {
      await reservationService.CancelHotelReservation(Data.HotelReservationId);
    }
  }

  public async Task HandleAsync(CreateCarReservation message, ISagaContext context)
  {
    var result = await reservationService.CreateCarReservation(message.UserId, message.CarId);
    if(!result.Created)
    {
      await RejectAsync(new Exception("Error creating car reservation"));
    }
    Data.CarReservationId = result.ReservationId;
  }
  public async Task CompensateAsync(CreateCarReservation message, ISagaContext context)
  {
    if(Data.CarReservationId != default)
    {
      await reservationService.CancelHotelReservation(Data.CarReservationId);
    }
  }

  public async Task HandleAsync(CompleteReservationSaga message, ISagaContext context)
  {
    message.FlyReservationId = Data.FlyReservationId;
    message.HotelReservationId = Data.HotelReservationId;
    message.CarReservationId = Data.CarReservationId;
    await CompleteAsync();
  }
  public Task CompensateAsync(CompleteReservationSaga message, ISagaContext context) => Task.CompletedTask;
}


using MediatR;

using Microservices.Gateway.Server.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.Gateway.Server.WebAPI;

internal static class ReservationAPI
{
  internal static void MapReservationEndpoints(this WebApplication app)
  {
    app.MapPost("/CreateReservation", CreateReservation);
    app.MapPost("/CreateReservationWithSaga", CreateReservationWithSaga);
  }

  private static async Task<IResult> CreateReservation([FromServices] IMediator mediator, Guid userId, Guid flyId, int SeatNumber, Guid CarId, Guid HotelId)
  {
    try
    {
      var result = await mediator.Send(new CreateReservationCommand
      {
        UserId = userId,
        FlyId = flyId,
        SeatNumber = SeatNumber,
        CarId = CarId,
        HotelId = HotelId
      });
      return result is null ? TypedResults.BadRequest("Sorry, but some error ocured") : TypedResults.Created(result.ToString());
    }
    catch(Exception ex)
    {
      return TypedResults.BadRequest(ex.Message);
    }
  }

  private static async Task<IResult> CreateReservationWithSaga([FromServices] IMediator mediator, Guid userId, Guid flyId, int SeatNumber, Guid CarId, Guid HotelId)
  {
    try
    {
      var result = await mediator.Send(new CreateReservationWithSagaCommand
      {
        UserId = userId,
        FlyId = flyId,
        SeatNumber = SeatNumber,
        CarId = CarId,
        HotelId = HotelId
      });
      return result is null ? TypedResults.BadRequest("Sorry, but some error ocured") : TypedResults.Ok(result);
    }
    catch(Exception ex)
    {
      return TypedResults.BadRequest(ex.Message);
    }
  }
}

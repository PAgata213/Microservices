using MediatR;

using Microservices.Gateway.Server.Sagas;
using Microservices.Gateway.Server.SagaWithRabbitMq;
using Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga;
using Microservices.Gateway.Server.SimpleHandler;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.Gateway.Server.WebAPI;

internal static class ReservationAPI
{
  internal static void MapReservationEndpoints(this WebApplication app)
  {
    app.MapPost("/CreateReservation", CreateReservation);
    app.MapPost("/CreateReservationWithSaga", CreateReservationWithSaga);
    app.MapPost("/CreateReservationWithConsumerSaga", CreateReservationWithConsumerSagaCommand);
  }

  private static async Task<IResult> CreateReservation([FromServices] IMediator mediator, Guid? userId, Guid? flyId, int? SeatNumber, Guid? CarId, Guid? HotelId)
  {
    try
    {
      var result = await mediator.Send(new CreateReservationCommand
      {
        UserId = userId ?? Guid.NewGuid(),
        FlyId = flyId ?? Guid.NewGuid(),
        SeatNumber = SeatNumber ?? 1,
        CarId = CarId ?? Guid.NewGuid(),
        HotelId = HotelId ?? Guid.NewGuid()
      });
      return result is null ? TypedResults.BadRequest("Sorry, but some error ocured") : TypedResults.Created("", result);
    }
    catch(Exception ex)
    {
      return TypedResults.BadRequest(ex.Message);
    }
  }

  private static async Task<IResult> CreateReservationWithSaga([FromServices] IMediator mediator, Guid? userId, Guid? flyId, int? SeatNumber, Guid? CarId, Guid? HotelId)
  {
    try
    {
      var result = await mediator.Send(new CreateReservationWithSagaCommand
      {
        UserId = userId ?? Guid.NewGuid(),
        FlyId = flyId ?? Guid.NewGuid(),
        SeatNumber = SeatNumber ?? 1,
        CarId = CarId ?? Guid.NewGuid(),
        HotelId = HotelId ?? Guid.NewGuid()
      });
      return result is null ? TypedResults.BadRequest("Sorry, but some error ocured") : TypedResults.Created("", result);
    }
    catch(Exception ex)
    {
      return TypedResults.BadRequest(ex.Message);
    }
  }

  private static async Task<IResult> CreateReservationWithConsumerSagaCommand([FromServices] IMediator mediator, Guid? userId, Guid? flyId, int? SeatNumber, Guid? CarId, Guid? HotelId)
  {
    await mediator.Send(new CreateReservationWithConsumerSagaCommand
    {
      UserId = userId ?? Guid.NewGuid(),
      FlyId = flyId ?? Guid.NewGuid(),
      SeatNumber = SeatNumber ?? 1,
      CarId = CarId ?? Guid.NewGuid(),
      HotelId = HotelId ?? Guid.NewGuid()
    });
    return Results.Accepted();
  }
}

using System.Runtime.CompilerServices;

using CarService.Commands;
using CarService.Models;
using CarService.Repository;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CarService.WebAPI;

internal static class CarWebAPI
{
  public static void MapCarEndpoints(this WebApplication app)
  {
    app.MapPost("/GetReservation/{reservationId}", GetReservation);
    app.MapPost("/CreateCarReservation", CreateCarReservation);
    app.MapPost("/CancelCarReservation", CancelCarReservation);
    app.MapPost("/ConfirmCarReservation", ConfirmCarReservation);
  }

  private static async Task<IResult> GetReservation([FromServices]ICarRepository carRepository, Guid reservationId)
  {
    var reservation = await carRepository.GetAsync(reservationId);
    return reservation is null ? Results.NotFound() : Results.Ok(reservation);
  }

  private static async Task<IResult> CreateCarReservation([FromServices]IMediator mediator, Guid userId, Guid carId)
  {
    var result = await mediator.Send(new CreateCarReservationCommand
    {
      UserId = userId,
      CarId = carId
    });
    return result is not null ? Results.Created($"/GetReservation/{result.Value}", result.Value) : Results.BadRequest();
  }

  private static async Task<IResult> CancelCarReservation([FromServices]IMediator mediator, Guid ReservationId)
  {
    await mediator.Send(new CancelCarReservationCommand
    {
      ReservationId = ReservationId
    });
    return Results.Ok();
  }

  private static async Task<IResult> ConfirmCarReservation([FromServices]IMediator mediator, Guid ReservationId)
  {
    await mediator.Send(new ConfirmCarReservationCommand
    {
      ReservationId = ReservationId
    });
    return Results.Ok();
  }
}

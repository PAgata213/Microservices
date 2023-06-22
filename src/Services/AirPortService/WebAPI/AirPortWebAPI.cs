using System.Runtime.CompilerServices;

using AirPortService.Commands;
using AirPortService.Models;
using AirPortService.Repository;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AirPortService.WebAPI;

internal static class AirPortWebAPI
{
  public static void MapAirPortEndpoints(this WebApplication app)
  {
    app.MapPost("/GetReservation/{reservationId}", GetReservation);
    app.MapPost("/CreateFlyReservation", CreateFlyReservation);
    app.MapPost("/CancelFlyReservation/{reservationId}", CancelFlyReservation);
    app.MapPost("/ConfirmFlyReservation/{reservationId}", ConfirmFlyReservation);
  }

  private static async Task<IResult> GetReservation([FromServices]IAirPortRepository airPortRepository, Guid reservationId)
  {
    var reservation = await airPortRepository.GetAsync(reservationId);
    return reservation is null ? Results.NotFound() : Results.Ok(reservation);
  }

  private static async Task<IResult> CreateFlyReservation([FromServices]IMediator mediator, CreateFlyReservationCommand command)
  {
    var result = await mediator.Send(command);
    return result is not null ? TypedResults.Created($"/GetReservation/{result.Value}", result.Value) : Results.BadRequest();
  }

  private static async Task<IResult> CancelFlyReservation([FromServices]IMediator mediator, Guid reservationId)
  {
    await mediator.Send(new CancelFlyReservationCommand
    {
      ReservationId = reservationId
    });
    return Results.Ok();
  }

  private static async Task<IResult> ConfirmFlyReservation([FromServices]IMediator mediator, Guid ReservationId)
  {
    await mediator.Send(new ConfirmFlyReservationCommand
    {
      ReservationId = ReservationId
    });
    return Results.Ok();
  }
}

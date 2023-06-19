using System.Runtime.CompilerServices;

using HotelService.Commands;
using HotelService.Models;
using HotelService.Repository;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelService.WebAPI;

internal static class HotelWebAPI
{
  public static void MapHotelEndpoints(this WebApplication app)
  {
    app.MapPost("/GetReservation/{reservationId}", GetReservation);
    app.MapPost("/CreateHotelReservation", CreateHotelReservation);
    app.MapPost("/CancelHotelReservation", CancelHotelReservation);
    app.MapPost("/ConfirmHotelReservation", ConfirmHotelReservation);
  }

  private static async Task<IResult> GetReservation([FromServices]IHotelRepository HotelRepository, Guid reservationId)
  {
    var reservation = await HotelRepository.GetAsync(reservationId);
    return reservation is null ? Results.NotFound() : Results.Ok(reservation);
  }

  private static async Task<IResult> CreateHotelReservation([FromServices]IMediator mediator, Guid userId, Guid hotelId)
  {
    var result = await mediator.Send(new CreateHotelReservationCommand
    {
      UserId = userId,
      HotelId = hotelId
    });
    return result is not null ? Results.Created($"/GetReservation/{result.Value}", result.Value) : Results.BadRequest();
  }

  private static async Task<IResult> CancelHotelReservation([FromServices]IMediator mediator, Guid ReservationId)
  {
    await mediator.Send(new CancelHotelReservationCommand
    {
      ReservationId = ReservationId
    });
    return Results.Ok();
  }

  private static async Task<IResult> ConfirmHotelReservation([FromServices]IMediator mediator, Guid ReservationId)
  {
    await mediator.Send(new ConfirmHotelReservationCommand
    {
      ReservationId = ReservationId
    });
    return Results.Ok();
  }
}

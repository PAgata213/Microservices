using MediatR;

using Microservices.Gateway.Server.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.Gateway.Server.WebAPI;

internal static class ReservationAPI
{
  internal static void MapReservationEndpoints(this WebApplication app)
  {
    app.MapPost("/CreateReservation", CreateReservation);
  }

  private static async Task<IResult> CreateReservation([FromServices] IMediator mediator, Guid userId, Guid flyId, int SeatNumber, Guid CarId, Guid HotelId)
  {
    await mediator.Send(new CreateReservationCommand
    {
      UserId = userId,
      FlyId = flyId,
      SeatNumber = SeatNumber,
      CarId = CarId,
      HotelId = HotelId
    });

    return TypedResults.Created();
  }
}

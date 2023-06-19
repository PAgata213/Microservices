using MediatR;

using Microservices.Gateway.Server.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Microservices.Gateway.Server.WebAPI;

internal static class ReservationAPI
{
  internal static void MapReservationEndpoints(WebApplication app)
  {
    app.MapPost("/CreateReservation", CreateReservation);
  }

  private static async Task<IResult> CreateReservation([FromServices] IMediator mediator, Guid userId, DateTime fromDate, DateTime toDate)
  {
    await mediator.Send(new CreateReservationCommand { UserId = userId, FromDate = fromDate, ToDate = toDate });

    return Results.Accepted();
  }
}

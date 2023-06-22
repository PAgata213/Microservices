using System.Net.Http;

using MediatR;

using Microservices.Gateway.Server.Commands;
using Microservices.Gateway.Server.Helpers;

namespace Microservices.Gateway.Server.Handlers;

public class CreateReservationCommandHandler(IHttpClientHelper httpClientHelper) : IRequestHandler<CreateReservationCommand>
{
  public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
  {
    //Create fly reservation
    var createFlyResponse = await httpClientHelper.PostAsync<CreateFlyReservationCommand, Guid>($"http://AirPortService-node:5100/CreateFlyReservation", new CreateFlyReservationCommand 
    {
      UserId = request.UserId,
      FlyId = request.FlyId,
      SeatNumber = request.SeatNumber
    });

    if(!createFlyResponse.IsSuccess)
    {
      throw new Exception("Error creating fly reservation");
    }
  }
}

public record CreateFlyReservationCommand
{
  public Guid FlyId { get; set; }
  public Guid UserId { get; set; }
  public int SeatNumber { get; set; }
}
using MediatR;
using Microservices.Gateway.Server.Models;

namespace Microservices.Gateway.Server.Commands;

public record CreateReservationWithSagaCommand : IRequest<ReservetionData?>
{
  public required Guid UserId { get; init; }
  public required Guid FlyId { get; init; }
  public required int SeatNumber { get; init; }
  public required Guid CarId { get; init; }
  public required Guid HotelId { get; init; }
}
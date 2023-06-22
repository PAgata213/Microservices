using MediatR;

namespace Microservices.Gateway.Server.Commands;

public record CreateReservationCommand : IRequest
{
  public required Guid UserId { get; init; }
  public required Guid FlyId { get; init; }
  public required int SeatNumber { get; init; }
  public required Guid CarId { get; init; }
  public required Guid HotelId { get; init; }
}

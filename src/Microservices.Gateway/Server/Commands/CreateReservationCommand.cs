using MediatR;

namespace Microservices.Gateway.Server.Commands;

public class CreateReservationCommand : IRequest
{
  public Guid UserId { get; set; }
  public DateTime FromDate { get; set; }
  public DateTime ToDate { get; set; }
}

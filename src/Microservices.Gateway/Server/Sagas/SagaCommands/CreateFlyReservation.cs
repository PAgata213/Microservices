namespace Microservices.Gateway.Server.Sagas.SagaCommands;

internal record CreateFlyReservation
{
  public Guid UserId { get; set; }
  public Guid FlyId { get; set; }
  public int SeatNumber { get; set; }
}


namespace Microservices.Gateway.Server.SagaCommands;

public record CompleteReservationSaga
{
  public Guid FlyReservationId { get; set; }
  public Guid HotelReservationId { get; set; }
  public Guid CarReservationId { get; set; }
}
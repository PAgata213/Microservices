namespace Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga;

public record ReservationFailed
{
  public Guid CorrelationId { get; set; }
  public Guid UserId { get; set; }
  public Guid FlyId { get; set; }
  public Guid HotelId { get; set; }
  public Guid CarId { get; set; }
  public string? ErrorMessage { get; set; }
}
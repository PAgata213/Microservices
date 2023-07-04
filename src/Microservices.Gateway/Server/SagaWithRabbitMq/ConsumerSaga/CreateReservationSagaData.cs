namespace Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga;

internal record CreateReservationSagaData
{
  public Guid FlyReservationId { get; set; }
  public Guid HotelReservationId { get; set; }
  public Guid CarReservationId { get; set; }
}

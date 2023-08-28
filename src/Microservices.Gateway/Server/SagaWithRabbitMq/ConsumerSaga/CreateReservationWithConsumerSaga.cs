using MassTransit;

using Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Commands;
using Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga.Events;
using Microservices.Gateway.Shared.ConsumerSaga.Commands;
using Microservices.Gateway.Shared.ConsumerSaga.Events;

namespace Microservices.Gateway.Server.SagaWithRabbitMq.ConsumerSaga;

public class CreateReservationWithConsumerSaga
  : ISaga,
  InitiatedBy<StartReservationConsumerSaga>,
  Orchestrates<FlyReservationCreated>,
  Orchestrates<FlyReservationCreationFailed>,
  Orchestrates<HotelReservationCreated>,
  Orchestrates<HotelReservationCreationFailed>,
  Orchestrates<CarReservationCreated>,
  Orchestrates<CarReservationCreationFailed>
{
  public Guid CorrelationId { get; set; }

  public StartReservationConsumerSaga? SagaData { get; set; }

  public Guid FlyReservationId { get; set; }
  public Guid HotelReservationId { get; set; }
  public Guid CarReservationId { get; set; }

  public async Task Consume(ConsumeContext<StartReservationConsumerSaga> context)
  {
    SagaData = context.Message;
    CorrelationId = context.Message.CorrelationId;
    await context.Publish(new CreateFlyReservation
    {
      CorrelationId = CorrelationId,
      UserId = SagaData.UserId,
      FlyId = SagaData.FlyId,
      SeatNumber = SagaData.SeatNumber
    }, context.CancellationToken);
  }

  public async Task Consume(ConsumeContext<FlyReservationCreated> context)
  {
    FlyReservationId = context.Message.ReservationId;
    await context.Publish(new CreateHotelReservation
    {
      CorrelationId = CorrelationId,
      UserId = SagaData!.UserId,
      HotelId = SagaData!.HotelId
    }, context.CancellationToken);
  }
  public async Task Consume(ConsumeContext<FlyReservationCreationFailed> context)
  {
    await FinalizeSagaWithError(context, context.Message.Message);
  }

  public async Task Consume(ConsumeContext<HotelReservationCreated> context)
  {
    HotelReservationId = context.Message.ReservationId;
    await context.Publish(new CreateCarReservation
    {
      CorrelationId = CorrelationId,
      UserId = SagaData!.UserId,
      CarId = SagaData!.CarId
    }, context.CancellationToken);
  }
  public async Task Consume(ConsumeContext<HotelReservationCreationFailed> context)
  {
    await context.Publish(new CancelFlyReservation
    {
      CorrelationId = CorrelationId,
      UserId = SagaData!.UserId,
      ReservationId = FlyReservationId
    }, context.CancellationToken);
    await FinalizeSagaWithError(context, context.Message.Message);
  }

  public async Task Consume(ConsumeContext<CarReservationCreated> context)
  {
    CarReservationId = context.Message.ReservationId;
    await context.Publish(new ReservationCreated
    {
      CorrelationId = CorrelationId,
      UserId = SagaData!.UserId,
      FlyReservationId = FlyReservationId,
      HotelReservationId = HotelReservationId,
      CarReservationId = CarReservationId
    }, context.CancellationToken);
  }
  public async Task Consume(ConsumeContext<CarReservationCreationFailed> context)
  {
    await context.Publish(new CancelHotelReservation
    {
      CorrelationId = CorrelationId,
      ReservationId = HotelReservationId
    }, context.CancellationToken);

    await context.Publish(new CancelFlyReservation
    {
      CorrelationId = CorrelationId,
      ReservationId = FlyReservationId
    }, context.CancellationToken);

    await FinalizeSagaWithError(context, context.Message.Message);
  }

  private async Task FinalizeSagaWithError<TMessage>(ConsumeContext<TMessage> context, string? errorMessage) where TMessage : class
    => await context.Publish(new ReservationFailed
    {
      CorrelationId = CorrelationId,
      UserId = SagaData!.UserId,
      FlyId = SagaData!.FlyId,
      HotelId = SagaData!.HotelId,
      CarId = SagaData!.CarId,
      ErrorMessage = errorMessage
    }, context.CancellationToken);
}

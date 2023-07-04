using Chronicle;

using MediatR;
using Microservices.Gateway.Server.Models;
using Microservices.Gateway.Server.Sagas.SagaCommands;

namespace Microservices.Gateway.Server.Sagas;

public class CreateReservationWithSagaCommandHandler(ISagaCoordinator sagaCoordinator) : IRequestHandler<CreateReservationWithSagaCommand, ReservetionData?>
{
    public async Task<ReservetionData?> Handle(CreateReservationWithSagaCommand request, CancellationToken cancellationToken)
    {
        var ctx = SagaContext.Create()
          .WithSagaId(SagaId.NewSagaId())
          .Build();

        await sagaCoordinator.ProcessAsync(new CreateFlyReservation
        {
            UserId = request.UserId,
            FlyId = request.FlyId,
            SeatNumber = request.SeatNumber
        }, ctx);

        await sagaCoordinator.ProcessAsync(new CreateHotelReservation
        {
            UserId = request.UserId,
            HotelId = request.HotelId
        }, ctx);

        await sagaCoordinator.ProcessAsync(new CreateCarReservation
        {
            UserId = request.UserId,
            CarId = request.CarId
        }, ctx);

        var data = new CompleteReservationSaga();
        await sagaCoordinator.ProcessAsync(data, ctx);

        var reservationData = new ReservetionData
        {
            Id = Guid.NewGuid(),
            FlyReservationId = data.FlyReservationId,
            HotelReservationId = data.HotelReservationId,
            CarReservationId = data.CarReservationId
        };

        return reservationData;
    }
}

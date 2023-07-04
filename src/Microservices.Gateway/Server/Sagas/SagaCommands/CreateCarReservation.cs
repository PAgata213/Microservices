namespace Microservices.Gateway.Server.Sagas.SagaCommands;

internal record CreateCarReservation
{
    public Guid UserId { get; set; }
    public Guid CarId { get; set; }
}


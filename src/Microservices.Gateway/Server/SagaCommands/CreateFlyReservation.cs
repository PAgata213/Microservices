namespace Microservices.Gateway.Server.SagaCommands;

internal record CreateFlyReservation
{
    public Guid UserId { get; set; }
    public Guid FlyId { get; set; }
    public int SeatNumber { get; set; }
}


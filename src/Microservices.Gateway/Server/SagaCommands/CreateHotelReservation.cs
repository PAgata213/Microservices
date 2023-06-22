namespace Microservices.Gateway.Server.SagaCommands;

internal record CreateHotelReservation
{
    public Guid UserId { get; set; }
    public Guid HotelId { get; set; }
}


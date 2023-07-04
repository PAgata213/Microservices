namespace AirPortService.Exceptions;

public class ReservationForGivenFlyAndSeatAlreadyExists(Guid flyId, int seatNumber) : Exception($"Reservation for fly {flyId} and seat {seatNumber} already exists");

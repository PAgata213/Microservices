namespace HotelService.Exceptions;

public class ReservationForGivenHotelAlreadyExists(Guid hotelId) : Exception($"Reservation for hotel {hotelId} already exists");

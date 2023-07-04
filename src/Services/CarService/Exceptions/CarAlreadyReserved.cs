namespace CarService.Exceptions;

public class CarAlreadyReserved(Guid carId) : Exception($"Reservation for car {carId} already exists");

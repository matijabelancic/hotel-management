namespace Api.Utilities.Exceptions;

public sealed class HotelNotFoundException(Guid hotelId) : NotFoundException($"The hotel with id: {hotelId} doesn't exist");
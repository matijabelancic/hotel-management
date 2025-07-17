using Api.Dtos;
using Api.Entities;

namespace Api.Mappings;

public static class DtoToEntityMapper
{
    public static Hotel ToEntity(this HotelRequest hotel, Guid? id = null)
    {
        return new Hotel
        {
            Id = id ?? Guid.NewGuid(),
            Name = hotel.Name,
            Price = hotel.Price,
            Latitude = hotel.Latitude,
            Longitude = hotel.Longitude
        };
    }
}
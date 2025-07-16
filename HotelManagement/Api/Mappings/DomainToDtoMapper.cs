using Api.Dtos;
using Api.Entities;
using Api.Utilities;

namespace Api.Mappings;

public static class DomainToDtoMapper
{
    public static HotelDto ToHotelDto(this Hotel hotel, double currentLatitude, double currentLongitude)
    {
        return new HotelDto
        {
            Name = hotel.Name,
            Price = hotel.Price,
            DistanceFromCurrentLocation = 
                GeoLocationHelper.CalculateDistance(hotel.Latitude, 
                    hotel.Longitude, 
                    currentLatitude, 
                    currentLongitude)
        };
    }
}

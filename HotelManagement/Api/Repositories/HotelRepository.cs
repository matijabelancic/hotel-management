using Api.Entities;
using Api.Interfaces;
using Api.Utilities.Exceptions;

namespace Api.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly List<Hotel> _hotels =
    [
        new Hotel { Id = Guid.Parse("b41cf367-872a-4a94-9560-8d4a9d33d1e4"), Name = "Hotel Split", Price = 85, Latitude = 43.5081, Longitude = 16.4402 }, 
        new Hotel { Id = Guid.Parse("8a1c6e7e-930f-4ae9-95c0-e80b47c7e2cb"), Name = "Hotel Rijeka", Price = 95, Latitude = 45.3271, Longitude = 14.4422 }, 
        new Hotel { Id = Guid.Parse("4fcb5bb2-c5fc-4051-b69e-f9e6e04c6fc0"), Name = "Hotel Zadar", Price = 120, Latitude = 44.1194, Longitude = 15.2314 }, 
        new Hotel { Id = Guid.Parse("e5b8a989-ec61-496c-ae9f-4f0bc2c7dba1"), Name = "Hotel Hvar", Price = 130, Latitude = 43.0657, Longitude = 16.2389 }, 
        new Hotel { Id = Guid.Parse("d962d2a7-0081-44a0-9e7f-5b9b384d9c2e"), Name = "Hotel Zagreb", Price = 110, Latitude = 45.8150, Longitude = 15.9819 },  
        new Hotel { Id = Guid.Parse("bcdf90b6-c1e4-4861-b7a0-8289e4e754f3"), Name = "Hotel Šibenik", Price = 90, Latitude = 44.7410, Longitude = 15.2158 }, 
        new Hotel { Id = Guid.Parse("9f3147db-41b1-4660-91ff-c7b1fd78e399"), Name = "Hotel Plitvice", Price = 80, Latitude = 45.5898, Longitude = 15.6000 }, 
        new Hotel { Id = Guid.Parse("d42e0502-44fd-4c0b-9c2e-7a6f58c426e3"), Name = "Hotel Zadar", Price = 75, Latitude = 44.1194, Longitude = 15.2278 }, 
        new Hotel { Id = Guid.Parse("093e7158-0f84-4b7c-bc35-529d8e50a8c4"), Name = "Hotel Dubrovnik", Price = 105, Latitude = 42.6507, Longitude = 18.0944 },
        new Hotel { Id = Guid.Parse("8b2b3b5d-0c84-4013-8c8f-31cb4762928e"), Name = "Hotel Pula", Price = 115, Latitude = 45.2396, Longitude = 13.9050 } 
    ];
    
    public IQueryable<Hotel> GetAll() => _hotels.AsQueryable();
    public Hotel GetById(Guid id)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == id);
        
        if (hotel == null)
            throw new HotelNotFoundException(id);

        return hotel;
    }

    public void Create(Hotel hotel) => _hotels.Add(hotel);
    
    public void Update(Hotel hotel)
    {
        var existingHotel = _hotels.FirstOrDefault(h => h.Id == hotel.Id);
        if (existingHotel == null)
            throw new HotelNotFoundException(hotel.Id);

        _hotels[_hotels.IndexOf(existingHotel)] = hotel;
    }

    public void Delete(Guid id)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == id);
       
        if (hotel == null)
            throw new HotelNotFoundException(id);
        
        _hotels.Remove(hotel);
    }
    
}
using Api.Dtos;
using Api.Entities;
using Api.Utilities;

namespace Api.Interfaces;

public interface IHotelService
{
    IEnumerable<Hotel> GetAll();
    Hotel Get (Guid id);
    Hotel Create(HotelRequest request);
    void Update(Guid id, HotelRequest request);
    void Delete(Guid id);
    PagedList<HotelDto> Search (SearchRequest request);
}
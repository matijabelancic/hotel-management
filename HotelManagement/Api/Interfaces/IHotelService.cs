using Api.Dtos;
using Api.Entities;
using Api.Utilities;

namespace Api.Interfaces;

public interface IHotelService
{
    PagedList<HotelDto> Search (SearchRequest request);
}
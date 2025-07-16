using Api.Dtos;
using Api.Entities;
using Api.Utilities;

namespace Api.Interfaces;

public interface IHotelRepository
{
    public IQueryable<Hotel> GetAll();
    Hotel GetById(Guid id);
    void Create(Hotel hotel);
    void Update(Hotel hotel);
    void Delete(Guid id);
}
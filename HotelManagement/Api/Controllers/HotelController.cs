using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelController(IHotelRepository repository, IHotelService hotelService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var hotel = repository.GetById(id);
        return Ok(hotel);
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var hotels = repository.GetAll();
        return Ok(hotels);
    }
    
    [HttpGet("Search")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public IActionResult Search([FromQuery] SearchRequest request)
    {
        var pagedResult = hotelService.Search(request);

        var response = new
        {
            pagedResult,
            pagedResult.MetaData.CurrentPage,
            pagedResult.MetaData.PageSize,
            pagedResult.MetaData.TotalPages,
            pagedResult.MetaData.TotalCount
        };
        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Hotel hotel)
    {
        repository.Create(hotel);
        return Created($"/hotels/{hotel.Id}", hotel);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update([FromBody] Hotel hotel)
    {
        repository.Update(hotel);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        repository.Delete(id);
        return NoContent();
    }
}

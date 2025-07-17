using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelsController(IHotelService hotelService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Hotel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var hotel = hotelService.Get(id);
        return Ok(hotel);
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var hotels = hotelService.GetAll();
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
    public IActionResult Create([FromBody] HotelRequest request)
    {
        var hotel = hotelService.Create(request);
        return Created($"/hotels/{hotel.Id}", hotel);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(Guid id, [FromBody] HotelRequest request)
    {
        hotelService.Update(id, request);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        hotelService.Delete(id);
        return NoContent();
    }
}

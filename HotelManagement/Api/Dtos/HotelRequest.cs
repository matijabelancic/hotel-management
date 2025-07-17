using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class HotelRequest
{
    public required string Name { get; set; }
    public required double Price { get; set; }
    [Range(0.000001, 90, ErrorMessage = "Latitude cannot be 0.")]
    public required double Latitude { get; set; }
    [Range(0.000001, 90, ErrorMessage = "Latitude cannot be 0.")]
    public required double Longitude { get; set; }
}
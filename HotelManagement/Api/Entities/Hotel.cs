using System.ComponentModel.DataAnnotations;

namespace Api.Entities;

public class Hotel
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required double Price { get; set; }
    
    public required double Latitude { get; set; }
   
    [Range(0.000001, 180, ErrorMessage = "Longitude cannot be 0.")]
    public required double Longitude { get; set; }
}
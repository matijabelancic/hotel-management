namespace Api.Dtos;

public class HotelDto
{
    public string? Name { get; set; }
    public double Price { get; set; }
    public double DistanceFromCurrentLocation { get; set; }
}
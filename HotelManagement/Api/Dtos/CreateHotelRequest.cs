namespace Api.Dtos;

public class CreateHotelRequest
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
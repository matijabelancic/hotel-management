using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Api.Services;
using Api.Utilities;
using Api.Utilities.Exceptions;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests;

public class HotelServiceTests
{
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;
    private readonly IOptions<HotelSearchWeights> _weightsOptions;
    private readonly HotelService _hotelService;

    public HotelServiceTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();

        _weightsOptions = Options.Create(new HotelSearchWeights
        {
            PriceWeight = 0.5,
            DistanceWeight = 0.5
        });

        _hotelService = new HotelService(_hotelRepositoryMock.Object, _weightsOptions);
    }

    private List<Hotel> GetFakeHotels() => new()
    {
        new Hotel { Id = Guid.NewGuid(), Name = "Cheap Close", Price = 50, Latitude = 45.0, Longitude = 15.0 },
        new Hotel { Id = Guid.NewGuid(), Name = "Expensive Far", Price = 200, Latitude = 40.0, Longitude = 10.0 }
    };

    [Fact]
    public void Search_ShouldThrow_WhenWeightsAreZero()
    {
        var zeroWeights = Options.Create(new HotelSearchWeights
        {
            PriceWeight = 0,
            DistanceWeight = 0
        });

        var service = new HotelService(_hotelRepositoryMock.Object, zeroWeights);

        var request = new SearchRequest
        {
            CurrentLatitude = 45,
            CurrentLongitude = 15,
            PageNumber = 1,
            PageSize = 10
        };
        
        Assert.Throws<PriceAndDistanceWeightZeroException>(() => service.Search(request));
    }

    [Fact]
    public void Search_ShouldReturnHotelsSortedByCombinedScore()
    {
        var hotels = GetFakeHotels();
        _hotelRepositoryMock.Setup(r => r.GetAll()).Returns(hotels.AsQueryable());

        var request = new SearchRequest
        {
            CurrentLatitude = 45,
            CurrentLongitude = 15,
            PageNumber = 1,
            PageSize = 10
        };
        
        var result = _hotelService.Search(request);
        
        Assert.Equal(2, result.Count);
        Assert.Equal("Cheap Close", result[0].Name);
        Assert.Equal("Expensive Far", result[1].Name);
    }

    [Fact]
    public void Search_ShouldReturnPagedResults()
    {
        var hotels = Enumerable.Range(1, 20).Select(i => new Hotel
        {
            Id = Guid.NewGuid(),
            Name = $"Hotel {i}",
            Price = i * 10,
            Latitude = 45.0,
            Longitude = 15.0
        }).ToList();

        _hotelRepositoryMock.Setup(r => r.GetAll()).Returns(hotels.AsQueryable());

        var request = new SearchRequest
        {
            CurrentLatitude = 45,
            CurrentLongitude = 15,
            PageNumber = 2,
            PageSize = 5
        };
        
        var result = _hotelService.Search(request);
        
        Assert.Equal(5, result.Count);
        Assert.Equal("Hotel 6", result[0].Name);
    }
}

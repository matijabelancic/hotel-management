using Api.Dtos;
using Api.Interfaces;
using Api.Mappings;
using Api.Utilities;
using Api.Utilities.Exceptions;
using Microsoft.Extensions.Options;

namespace Api.Services;

public class HotelService(IHotelRepository hotelRepository, IOptions<HotelSearchWeights> weightsConfig) : IHotelService
{
    public PagedList<HotelDto> Search(SearchRequest request)
    {
        if (weightsConfig.Value.PriceWeight + weightsConfig.Value.DistanceWeight == 0)
            throw new PriceAndDistanceWeightZeroException();
        
        var hotels = hotelRepository.GetAll();
        
        var hotelsDto = hotels
            .Select(x=> x.ToHotelDto(request.CurrentLatitude, request.CurrentLongitude))
            .OrderBy(x => x.Price * weightsConfig.Value.PriceWeight + x.DistanceFromCurrentLocation * weightsConfig.Value.DistanceWeight)
            .ToList();
        
        return PagedList<HotelDto>.ToPagedList(hotelsDto, request.PageNumber, request.PageSize);
    }
}
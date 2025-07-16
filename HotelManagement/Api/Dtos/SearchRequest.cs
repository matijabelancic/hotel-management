using System.ComponentModel.DataAnnotations;
using Api.Utilities;

namespace Api.Dtos;

public class SearchRequest : RequestPaginationParameters
{
    [Range(0.000001, 90, ErrorMessage = "Latitude cannot be 0.")]
    public required double CurrentLatitude { get; set; }
    
    [Range(0.000001, 180, ErrorMessage = "Longitude cannot be 0.")]
    public required double CurrentLongitude { get; set; }
} 
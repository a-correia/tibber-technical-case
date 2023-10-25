using System.ComponentModel.DataAnnotations;

namespace CleaningService.Api.Requests;

public class CoordinatesDto
{
    [Range(-100000, 100000, ErrorMessage = "X coordinate must be between -100000 and 100000.")]
    public int X { get; set; }

    [Range(-100000, 100000, ErrorMessage = "Y coordinate must be between -100000 and 100000.")]
    public int Y { get; set; }
}
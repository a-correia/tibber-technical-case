using System.ComponentModel.DataAnnotations;

namespace CleaningService.Api.Requests;

public class CommandDto
{
    [Required]
    [EnumDataType(typeof(DirectionEnumDto), ErrorMessage = "Invalid direction.")]
    public DirectionEnumDto Direction { get; set; }
    
    [Range(1, 99999, ErrorMessage = "Steps must be between 1 and 99999.")]
    public int Steps { get; set; }
}


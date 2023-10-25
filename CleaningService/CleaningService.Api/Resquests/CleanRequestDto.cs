using System.ComponentModel.DataAnnotations;

namespace CleaningService.Api.Requests;

public class CleanRequestDto
{
    [Required]
    public CoordinatesDto Start { get; set; }
    
    [Required]
    [MaxLength(10000, ErrorMessage = "Number of commands should not exceed 10,000.")]
    public List<CommandDto> Commands { get; set; }
}

namespace CleaningService.Domain.Model;

public class CleanResponseDto
{
    public CleanResponseDto() { }
    
    public CleanResponseDto(int commands, DateTime timestamp, int result, double duration)
    {
        Timestamp = timestamp;
        Commands = commands;
        Result = result;
        Duration = duration;
    }
    public int Id { get; set; }
    public DateTime Timestamp { get; }
    public int Commands { get; }
    public int Result { get; }
    public double Duration { get; }
}
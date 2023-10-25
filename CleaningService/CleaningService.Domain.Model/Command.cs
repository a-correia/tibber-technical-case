namespace CleaningService.Domain.Model;

public class Command
{
    public DirectionEnum Direction { get; set; }
    public int Steps { get; set; }
}


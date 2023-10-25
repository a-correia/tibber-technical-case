namespace CleaningService.Domain.Model;

public class CleanInput
{
    public Coordinates Start { get; set; }
    public List<Command> Commands { get; set; }
}

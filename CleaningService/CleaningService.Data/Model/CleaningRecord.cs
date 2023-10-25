using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleaningService.Repositories.Model;

public class CleaningRecord
{
    
    public CleaningRecord() { }
    
    public CleaningRecord(int commands, DateTime timestamp, int result, double duration)
    {
        Timestamp = timestamp;
        Commands = commands;
        Result = result;
        Duration = duration;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime Timestamp { get; }
    public int Commands { get; }
    public int Result { get; }
    public double Duration { get; }
}
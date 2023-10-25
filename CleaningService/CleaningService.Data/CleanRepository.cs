using CleaningService.Repositories.Model;
using Microsoft.Extensions.Logging;

namespace CleaningService.Repositories;
using System.Linq;

public class CleanRepository : ICleanRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<CleanRepository> _logger;


    public CleanRepository(AppDbContext context,  ILogger<CleanRepository> logger)
    {
        _context = context;
        _logger = logger;

    }

    public CleaningRecord? AddCleaningRecord(CleaningRecord? record)
    {
        _context.CleaningRecords.Add(record);
        _context.SaveChanges();
        _logger.LogInformation($"Cleaning execution record added successfully");
        return record;
    }

    public List<CleaningRecord> GetAllCleaningRecords()
    {
        _logger.LogInformation("Retrieving all cleaning records");
        return _context.CleaningRecords.ToList();
    }

    public CleaningRecord GetCleaningRecordById(int id)
    {
        _logger.LogInformation("Retrieving cleaning record by ID: {RecordId}", id);
        return _context.CleaningRecords.SingleOrDefault(record => record.Id == id);
    }
}
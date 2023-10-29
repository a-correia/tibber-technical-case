using CleaningService.Repositories.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleaningService.Repositories;

public class CleanRepository : ICleanRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<CleanRepository> _logger;


    public CleanRepository(AppDbContext context, ILogger<CleanRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CleaningRecord?> AddCleaningRecord(CleaningRecord? record)
    {
        _context.CleaningRecords.Add(record);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Cleaning execution record added successfully");
        return record;
    }

    public async Task<List<CleaningRecord>> GetAllCleaningRecords()
    {
        _logger.LogInformation("Retrieving all cleaning records");
        return await _context.CleaningRecords.ToListAsync();
    }

    public async Task<CleaningRecord> GetCleaningRecordById(int id)
    {
        _logger.LogInformation("Retrieving cleaning record by ID: {RecordId}", id);
        return await _context.CleaningRecords.SingleOrDefaultAsync(record => record.Id == id);
    }
}
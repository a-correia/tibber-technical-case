using CleaningService.Repositories.Model;

namespace CleaningService.Repositories;

public interface ICleanRepository
{
    public Task<CleaningRecord?> AddCleaningRecord(CleaningRecord? record);

    public Task<List<CleaningRecord>> GetAllCleaningRecords();

    public Task<CleaningRecord> GetCleaningRecordById(int id);
}
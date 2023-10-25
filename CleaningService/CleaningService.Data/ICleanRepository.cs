using CleaningService.Repositories.Model;

namespace CleaningService.Repositories;

public interface ICleanRepository
{
    CleaningRecord? AddCleaningRecord(CleaningRecord? record);

    public List<CleaningRecord> GetAllCleaningRecords();

    public CleaningRecord GetCleaningRecordById(int id);
}
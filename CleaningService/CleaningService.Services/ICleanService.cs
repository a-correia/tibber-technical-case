using CleaningService.Domain.Model;

namespace CleaningService.Services;

using CleaningService.Repositories.Model;

public interface ICleanService
{
    public Task<CleanOutput> Clean(CleanInput cleanRequest);

    public Task<List<CleanOutput>> GetCleanRecords();

    public Task<CleanOutput> GetCleanRecordById(int id);
}
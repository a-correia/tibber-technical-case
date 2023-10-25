
using CleaningService.Domain.Model;

namespace CleaningService.Services;
using CleaningService.Repositories.Model;

public interface ICleanService
{
    CleanOutput Clean(CleanInput cleanRequest);
    
    List<CleanOutput> GetCleanRecords();
    
    CleanOutput GetCleanRecordById(int id);

}
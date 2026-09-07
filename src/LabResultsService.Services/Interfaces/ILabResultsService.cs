using LabResultsService.Repository.Data.Models;
using LabResultsService.Services.DTOs;

namespace LabResultsService.Services.Interfaces
{
    public interface ILabResultsService
    {
        public Task<Guid> RecordLabResultAsync(RecordLabResultDTO _);
        public Task<LabResult?> GetLabResultByIdAsync(string _); 
        public Task<IEnumerable<LabResult>> FilterLabResultsByPatientIdAsync(string _, bool includeDeletedRecords = false);
        public Task<bool> SoftDeleteLabResultAsync(string _);
        public Task<bool> UpdateLabResult(string id, UpdateLabResultDTO _);
    }
}

using LabResultsService.Repository.Data.Models;
using LabResultsService.Services.DTOs;

namespace LabResultsService.Services.Interfaces
{
    public interface ILabResultsService
    {
        public Task<Guid> RecordLabResultAsync(RecordLabResultDTO request);
        public Task<LabResult?> GetLabResultByIdAsync(string id); 
        public Task<IEnumerable<LabResult>> FilterLabResultsByPatientIdAsync(string patientId, bool includeDeletedRecords = false);
        public Task<bool> SoftDeleteLabResultAsync(string id);
        public Task<bool> UpdateLabResult(string id, UpdateLabResultDTO request);
    }
}

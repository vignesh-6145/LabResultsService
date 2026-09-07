using LabResultsService.Repository.Data.Models;
using LabResultsService.Services.DTOs;

namespace LabResultsService.Services.Interfaces
{
    public interface ILabResultsService
    {
        public Task<Guid> RecordLabResultAsync(RecordLabResultDTO _);
        public Task<LabResult?> GetLabResultById(string _); 
        public Task<IEnumerable<LabResult>> FilterLabResultsByPatientId(string _);
    }
}

using LabResultsService.Services.DTOs;

namespace LabResultsService.Services.Interfaces
{
    public interface IPatientService
    {
        public Task<Guid> CreatePatientRecordAsync(CreatePatientDTO request);
        public Task<bool> IsAValidUserAsync(string id);
    }
}

using LabResultsService.Services.ViewModels;

namespace LabResultsService.Services.Interfaces
{
    public interface IPatientService
    {
        public Task<Guid> CreatePatientRecordAsync(CreatePatientDTO request);
        public Task<bool> IsAValidUserAsync(string id);
    }
}

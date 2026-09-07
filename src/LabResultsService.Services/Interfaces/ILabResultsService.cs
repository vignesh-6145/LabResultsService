using LabResultsService.Services.DTOs;

namespace LabResultsService.Services.Interfaces
{
    public interface ILabResultsService
    {
        public Task<Guid> RecordLabResultAsync(RecordLabResultDTO _);
    }
}

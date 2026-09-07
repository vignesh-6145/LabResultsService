using LabResultsService.Repository.Interfaces;
using LabResultsService.Services.DTOs;
using LabResultsService.Services.Interfaces;
using LabResultsService.Services.ModelMapper;
using Microsoft.Extensions.Logging;

namespace LabResultsService.Services.Services
{
    public class LabResultsService(ILabResultsRepository _labResultsRepository, ILogger<LabResultsService> _logger) : ILabResultsService
    {
        public async Task<Guid> RecordLabResultAsync(RecordLabResultDTO request)
        {

            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(request.ResultValue, nameof(request.ResultValue));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(request.TestName, nameof(request.TestName));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(request.Unit, nameof(request.Unit));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(request.PatientId, nameof(request.PatientId));

            var labResultInfo = LabResultsModelMapper.FromRecordLabResultDTO(request);
            var labResultId = await _labResultsRepository.AddlabResultAsync(labResultInfo);

            if (labResultId != Guid.Empty)
            {
                _logger.LogInformation("Added LabResult of {TestName} for Patient ; {PatientId}", request.TestName, request.PatientId);
            }
            return labResultId;
        }
    }
}

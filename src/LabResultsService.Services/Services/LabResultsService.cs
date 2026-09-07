using LabResultsService.Repository.Data.Models;
using LabResultsService.Repository.Interfaces;
using LabResultsService.Services.DTOs;
using LabResultsService.Services.Interfaces;
using LabResultsService.Services.ModelMapper;
using Microsoft.Extensions.Logging;

namespace LabResultsService.Services.Services
{
    public class LabResultsService(ILabResultsRepository _labResultsRepository, ILogger<LabResultsService> _logger) : ILabResultsService
    {
        public async Task<IEnumerable<LabResult>> FilterLabResultsByPatientId(string patientId)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(patientId);

            var validGuid = Guid.TryParse(patientId, out var parsedId);

            if (!validGuid)
            {
                throw new InvalidDataException(patientId);
            }

            //TODO : check valid user
            return await _labResultsRepository.GetLabResultsBypatientIdAsync(parsedId);
        }

        public async Task<LabResult?> GetLabResultById(string id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

            var validGuid = Guid.TryParse(id, out var parsedId);

            if (!validGuid)
            {
                throw new InvalidDataException(id);
            }

            return await _labResultsRepository.GetLabResultsByIdAsync(parsedId);
        }

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

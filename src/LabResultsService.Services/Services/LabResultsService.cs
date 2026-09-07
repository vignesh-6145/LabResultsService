using LabResultsService.Core.Exceptions;
using LabResultsService.Repository.Data.Models;
using LabResultsService.Repository.Interfaces;
using LabResultsService.Services.DTOs;
using LabResultsService.Services.Interfaces;
using LabResultsService.Services.ModelMapper;
using Microsoft.Extensions.Logging;

namespace LabResultsService.Services.Services
{
    public class LabResultsService(ILabResultsRepository _labResultsRepository, IPatientService _patientService, ILogger<LabResultsService> _logger) : ILabResultsService
    {
        public async Task<bool> SoftDeleteLabResultAsync(string id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

            var validGuid = Guid.TryParse(id, out var parsedId);

            if (!validGuid)
            {
                throw new InvalidDataException(id);
            }

            var labResult = await _labResultsRepository.GetLabResultsByIdAsync(parsedId);
            if (labResult is null)
            {
                throw new ResourceNotFoundException($"No matching {nameof(labResult)} record found for Id {id}");
            }
            labResult.IsActive = false;

            return await _labResultsRepository.UpdateLabResultAsync(labResult);
        }

        public async Task<IEnumerable<LabResult>> FilterLabResultsByPatientIdAsync(string patientId, bool includeDeletedRecords = false)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(patientId);

            var validGuid = Guid.TryParse(patientId, out var parsedId);

            if (!validGuid)
            {
                throw new InvalidDataException(patientId);
            }

            var userExists = await _patientService.IsAValidUser(patientId);
            if (!userExists)
            {
                _logger.LogInformation("No patient Exists with the given Id {Patientid}",patientId);
                throw new ResourceNotFoundException($"No matching patient record found for Id {patientId}");
            }

            var patientRecords = await _labResultsRepository.GetLabResultsBypatientIdAsync(parsedId);

            if (includeDeletedRecords)
                return patientRecords;

            return patientRecords.Where(record => record.IsActive);
        }

        public async Task<LabResult?> GetLabResultByIdAsync(string id)
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

        public async Task<bool> UpdateLabResult(string id, UpdateLabResultDTO updatedRecord)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

            var validGuid = Guid.TryParse(id, out var parsedId);

            if (!validGuid)
            {
                throw new InvalidDataException(id);
            }

            var existantRecord = await _labResultsRepository.GetLabResultsByIdAsync(parsedId);
            string originalValue = string.Empty;

            if (existantRecord is null)
            {
                throw new ResourceNotFoundException($"No amtching {nameof(existantRecord)} record found for Id {id}");
            }
            
            if (updatedRecord.PatientId.HasValue)
            {
                var userExists = await _patientService.IsAValidUser(updatedRecord.PatientId.Value.ToString());
                if (!userExists)
                {
                    _logger.LogInformation("No patient Exists with the given Id {Patientid}", updatedRecord.PatientId.Value.ToString());
                    throw new ResourceNotFoundException($"No matching patient record found for Id {updatedRecord.PatientId.Value.ToString()}");
                }

                originalValue = existantRecord.PatientId.ToString();
                existantRecord.PatientId = updatedRecord.PatientId.Value;
                _logger.LogInformation("Transferred the record from {OriginalPatientId} to {ModifiedPatientId}",originalValue, updatedRecord.PatientId);
            }

            if (!string.IsNullOrWhiteSpace(updatedRecord.TestName))
            {
                originalValue = existantRecord.TestName;
                existantRecord.TestName = updatedRecord.TestName;
                _logger.LogInformation("Modified the TestRecordName from {OriginalTestName} to {ModifiedTestName}", originalValue, updatedRecord.TestName);
            }

            if (!string.IsNullOrWhiteSpace(updatedRecord.ResultValue))
            {
                originalValue = existantRecord.ResultValue;
                existantRecord.ResultValue = updatedRecord.ResultValue;
                _logger.LogInformation("Modified the ResultValue from {OriginalResultValue} to {ModifiedResultValue}", originalValue, updatedRecord.ResultValue);
            }

            if (!string.IsNullOrWhiteSpace(updatedRecord.Unit))
            {
                originalValue = existantRecord.Unit;
                existantRecord.Unit = updatedRecord.Unit;
                _logger.LogInformation("Modified the Unit from {OriginalUnit} to {Modifiedunit}", originalValue, updatedRecord.Unit);
            }

            if (updatedRecord.ObservedDate.HasValue)
            {
                originalValue = existantRecord.ObservedDate.ToString();
                existantRecord.ObservedDate = updatedRecord.ObservedDate.Value;
                _logger.LogInformation("Modified the ObservedDate from {OriginalObservedDate} to {ModifiedObservedDate}", originalValue, updatedRecord.ObservedDate.ToString());
            }

            if (updatedRecord.IsActive.HasValue)
            {
                originalValue = existantRecord.IsActive.ToString();
                existantRecord.IsActive = updatedRecord.IsActive.Value;
            }

            return await _labResultsRepository.UpdateLabResultAsync(existantRecord);
        }
    }
}

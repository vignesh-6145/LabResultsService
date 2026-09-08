using LabResultsService.Repository.Interfaces;
using LabResultsService.Services.Interfaces;
using LabResultsService.Services.ModelMapper;
using LabResultsService.Services.Utils;
using LabResultsService.Services.ViewModels;
using Microsoft.Extensions.Logging;

namespace LabResultsService.Services.PatientServices
{
    public class PatientService(IPatientRepository _patientRepository, ILogger<PatientService> _logger) : IPatientService
    {
        public async Task<Guid> CreatePatientRecordAsync(CreatePatientDTO createPatientDto)
        {
            ArgumentNullException.ThrowIfNull(createPatientDto, nameof(createPatientDto));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(createPatientDto.FirstName, nameof(createPatientDto.FirstName));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(createPatientDto.LastName, nameof(createPatientDto.LastName));

            try
            {
                var patientDetails = PatientModelMapper.FromCreatePatientDto(createPatientDto);
                var patientUniqueId = await _patientRepository.AddPatientAsync(patientDetails);

                if (patientUniqueId != Guid.Empty) {
                    _logger.LogInformation("Patient Record is added to our records.");
                }

                return patientUniqueId;
            }

            catch (Exception ex) {
                _logger.LogError(ex,"Unable to create patient record {Message}",ex.Message);
            }

            return Guid.Empty;
        }

        public Task<bool> IsAValidUserAsync(string id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

            Guid parsedId = GuidUtils.ParseGuidOrThrow(id);

            return _patientRepository.PatientExistsAsync(parsedId);
        }
    }
}

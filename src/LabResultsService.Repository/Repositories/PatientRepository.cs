using LabResultsService.Repository.Data;
using LabResultsService.Repository.Data.Models;
using LabResultsService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabResultsService.Repository.Repositories
{
    public class PatientRepository(LabResultsDbContext _dbContext, ILogger<PatientRepository> _logger) : IPatientRepository
    {
        public async Task<Guid> AddPatientAsync(Patient patientDetails)
        {
            await _dbContext.Patients.AddAsync(patientDetails);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Patient {FirstName} added.", patientDetails.FirstName);

            return patientDetails.Id;
        }

        public async Task<bool> PatientExistsAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            return await _dbContext.Patients.AnyAsync(record => record.Id == id && record.IsActive);
        }
    }
}

using LabResultsService.Repository.Data;
using LabResultsService.Repository.Data.Models;
using LabResultsService.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Repository.Repositories
{
    public class PatientRepository(LabResultsDbContext _dbContext, ILogger<PatientRepository> _logger) : IPatientRepository
    {
        public async Task<Guid> AddPatientAsync(Patient patientDetails)
        {
            try
            {
                await _dbContext.Patients.AddAsync(patientDetails);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Patient {FirstName} added.",patientDetails.FirstName);

                return patientDetails.Id;
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to Insert patient Record. Message {ErrorMessage}",ex.InnerException);
            }
            return Guid.Empty;
        }
    }
}

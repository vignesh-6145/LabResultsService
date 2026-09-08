using LabResultsService.Repository.Data;
using LabResultsService.Repository.Data.Models;
using LabResultsService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabResultsService.Repository.Repositories
{
    public class LabResultRepository(LabResultsDbContext _dbContext, ILogger<LabResultRepository> _logger) : ILabResultsRepository
    {
        public async Task<Guid> AddLabResultAsync(LabResult labResultInformation)
        {
            if(labResultInformation.PatientId == Guid.Empty)
            {
                throw new InvalidOperationException("Can't add alient records");
            }

            //TODO : enclose in try catch
            await _dbContext.LabResults.AddAsync(labResultInformation);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("LabResult is added to our records");
            return labResultInformation.Id;
        }

        public async Task<LabResult?> GetLabResultsByIdAsync(Guid Id)
        {
            return await _dbContext.LabResults.FindAsync(Id);
        }

        public async Task<IEnumerable<LabResult>> GetLabResultsByPatientIdAsync(Guid patientId)
        {
            var patientRecords = await _dbContext.LabResults.Where(record => record.PatientId == patientId).ToListAsync();
            return patientRecords;
        }

        public async Task<bool> UpdateLabResultAsync(LabResult labRecordInfo)
        {
            try
            {
                _dbContext.LabResults.Update(labRecordInfo);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Changes were updated in the records");
                return true;
            }catch(DbUpdateException dex)
            {
                _logger.LogError(dex, "Failed to insert lab result. Database error.");
                throw new InvalidDataException("Unable to save lab result. Invalid data.", dex);
            }
        }
    }
}

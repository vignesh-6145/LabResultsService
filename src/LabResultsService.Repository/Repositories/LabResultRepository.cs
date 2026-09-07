using LabResultsService.Repository.Data;
using LabResultsService.Repository.Data.Models;
using LabResultsService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabResultsService.Repository.Repositories
{
    public class LabResultRepository(LabResultsDbContext _dbContext, ILogger<LabResultRepository> _logger) : ILabResultsRepository
    {
        public async Task<Guid> AddlabResultAsync(LabResult labResultInformation)
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

        public async Task<IEnumerable<LabResult>> GetLabResultsBypatientIdAsync(Guid patientId)
        {
            var patientRecords = await _dbContext.LabResults.Where(record => record.PatientId == patientId).ToListAsync();
            return patientRecords;
        }
    }
}

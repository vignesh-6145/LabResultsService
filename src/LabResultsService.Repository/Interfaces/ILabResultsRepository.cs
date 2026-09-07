using LabResultsService.Repository.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Repository.Interfaces
{
    public interface ILabResultsRepository
    {
        public Task<Guid> AddlabResultAsync(LabResult labResultInformation);
        public Task<IEnumerable<LabResult>> GetLabResultsBypatientIdAsync(Guid patientId);
        public Task<LabResult?> GetLabResultsByIdAsync(Guid Id);
    }
}

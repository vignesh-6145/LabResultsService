using LabResultsService.Repository.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Repository.Interfaces
{
    public interface ILabResultsRepository
    {
        public Task<Guid> AddLabResultAsync(LabResult labResultInformation);
        public Task<IEnumerable<LabResult>> GetLabResultsByPatientIdAsync(Guid patientId);
        public Task<LabResult?> GetLabResultsByIdAsync(Guid Id);
        public Task<bool> UpdateLabResultAsync(LabResult labRecordInfo);
    }
}

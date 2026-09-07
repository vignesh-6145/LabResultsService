using LabResultsService.Repository.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Repository.Interfaces
{
    public interface IPatientRepository
    {
        public Task<Guid> AddPatientAsync(Patient patientDetails);
        public Task<bool> UserExists(Guid Id);
    }
}

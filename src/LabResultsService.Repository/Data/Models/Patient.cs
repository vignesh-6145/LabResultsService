using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Repository.Data.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<LabResult> PatientTestResults = new List<LabResult>();
        public bool IsActive { get; set; }
    }
}

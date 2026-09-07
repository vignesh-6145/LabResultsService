using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Services.DTOs
{
    public class UpdateLabResultDTO
    {
        public Guid? PatientId { get; set; }
        public string? TestName { get; set; }
        public string? ResultValue { get; set; }
        public string? Unit { get; set; }
        public DateTime? ObservedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}

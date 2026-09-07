using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LabResultsService.Services.DTOs
{
    public class RecordLabResultDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatientId Is Required")]
        public string PatientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Test name is required.")]
        [MinLength(5, ErrorMessage = "Test name must contain at least 5 characters.")]
        [MaxLength(128, ErrorMessage = "Test name must not exceed 128 characters.")]
        public string TestName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Result is required.")]
        [MinLength(2, ErrorMessage = "Result must contain at least 5 characters.")]
        [MaxLength(256, ErrorMessage = "Result must not exceed 256 characters.")]
        public string ResultValue { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unit is required.")]
        [MinLength(3, ErrorMessage = "Unit must contain at least 3 characters.")]
        [MaxLength(32, ErrorMessage = "Unit must not exceed 32 characters.")]
        public string Unit { get; set; }
    }
}

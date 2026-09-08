using System.ComponentModel.DataAnnotations;

namespace LabResultsService.Services.DTOs
{
    public class RecordLabResultDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "PatientId Is Required")]
        public string PatientId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Test name is required.")]
        [MaxLength(128, ErrorMessage = "Test name must not exceed 128 characters.")]
        public string TestName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Result is required.")]
        [MaxLength(256, ErrorMessage = "Result must not exceed 256 characters.")]
        public string ResultValue { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unit is required.")]
        [MaxLength(32, ErrorMessage = "Unit must not exceed 32 characters.")]
        public string Unit { get; set; }

        public DateTime? ObservedDate { get; set; }
    }
}

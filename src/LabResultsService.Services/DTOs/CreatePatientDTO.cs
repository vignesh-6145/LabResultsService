using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LabResultsService.Services.ViewModels
{
    public class CreatePatientDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
        [MinLength(5, ErrorMessage = "First name must contain at least 5 characters.")]
        [MaxLength(126, ErrorMessage = "First name must not exceed 126 characters.")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
        [MinLength(5, ErrorMessage = "Last name must contain at least 5 characters.")]
        [MaxLength(126, ErrorMessage = "Last name must not exceed 126 characters.")]
        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}

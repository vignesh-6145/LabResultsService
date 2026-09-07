using LabResultsService.Repository.Data.Models;
using LabResultsService.Services.ViewModels;

namespace LabResultsService.Services.ModelMapper
{
    public static class PatientModelMapper
    {
        public static Patient FromCreatePatientDto(CreatePatientDTO dto) => new()
        {
             FirstName = dto.FirstName.Trim(),   
             LastName = dto.LastName.Trim(),
             MiddleName = dto.MiddleName.Trim(),
        };
    }
}

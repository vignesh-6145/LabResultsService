using LabResultsService.Repository.Data.Models;
using LabResultsService.Services.ViewModels;

namespace LabResultsService.Services.ModelMapper
{
    public static class PatientModelMapper
    {
        public static Patient FromCreatePatientDto(CreatePatientDTO dto) => new()
        {
             FirstName = dto.FirstName,   
             LastName = dto.LastName,
             MiddleName = dto.MiddleName,
        };
    }
}

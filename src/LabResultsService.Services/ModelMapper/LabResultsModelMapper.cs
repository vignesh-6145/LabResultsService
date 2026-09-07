using LabResultsService.Repository.Data.Models;
using LabResultsService.Services.DTOs;

namespace LabResultsService.Services.ModelMapper
{
    public static class LabResultsModelMapper
    {
        public static LabResult FromRecordLabResultDTO(RecordLabResultDTO dto) => new()
        {
            ObservedDate = DateTime.Now,
            PatientId = Guid.Parse(dto.PatientId),
            ResultValue = dto.ResultValue.Trim(),
            TestName = dto.TestName.Trim(),
            Unit = dto.TestName.Trim()
        };
    }
}

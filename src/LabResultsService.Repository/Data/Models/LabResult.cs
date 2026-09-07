namespace LabResultsService.Repository.Data.Models
{
    public class LabResult
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string TestName { get; set; }
        public string ResultValue { get; set; }
        public string Unit { get; set; }
        public DateTime ObservedDate { get; set; }
    }
}

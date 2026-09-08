namespace LabResultsService.Services.Utils
{
    public static class GuidUtils
    {
        public static Guid ParseGuidOrThrow(string patientId)
        {
            var validGuid = Guid.TryParse(patientId, out var parsedId);

            if (!validGuid || parsedId == Guid.Empty)
            {
                throw new InvalidDataException(patientId);
            }

            return parsedId;
        }
    }
}

namespace LabResultsService.Services.Utils
{
    public static class GuidUtils
    {
        public static Guid ParseGuidOrThrow(string id)
        {
            var validGuid = Guid.TryParse(id, out var parsedId);

            if (!validGuid || parsedId == Guid.Empty)
            {
                throw new InvalidDataException($"Invalid ID: '{id}'.");
            }

            return parsedId;
        }
    }
}

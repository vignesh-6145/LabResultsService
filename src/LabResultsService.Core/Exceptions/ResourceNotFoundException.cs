using System;
using System.Collections.Generic;
using System.Text;

namespace LabResultsService.Core.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string? message) : base(message)
        {
        }
    }
}

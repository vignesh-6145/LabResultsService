using LabResultsService.Services.Interfaces;
using LabResultsService.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LabResultsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController(IPatientService _patientService, ILogger<PatientsController> _logger) : ControllerBase
    {
        [HttpPost("createAPatientRecord")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO request)
        {
            try
            {
                var patientId = await _patientService.CreatePatientRecordAsync(request);
                return patientId == Guid.Empty ? BadRequest() : Ok(patientId);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Something went wrong while creating patient record. Message {ErrorMessage}",ex.InnerException);
            }
            return BadRequest();

        }

    }
}

using LabResultsService.Services.DTOs;
using LabResultsService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LabResultsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController(IPatientService _patientService, ILogger<PatientsController> _logger) : ControllerBase
    {
        [HttpPost("createAPatientRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO request)
        {
            try
            {
                var patientId = await _patientService.CreatePatientRecordAsync(request);
                return patientId == Guid.Empty ? BadRequest() : Ok(patientId);
            }
            catch (Exception aex) when (aex is ArgumentNullException || aex is InvalidDataException)
            {
                _logger.LogError(aex, "Invalid Data Found. Message {ErrorMessage}", aex.Message);
                return BadRequest(aex.Message);
            }
        }

    }
}

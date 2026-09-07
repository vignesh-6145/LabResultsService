using LabResultsService.Services.DTOs;
using LabResultsService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LabResultsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabResultsController(ILabResultsService _labResultsService, ILogger<LabResultsController> _logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RecordLabResults([FromBody] RecordLabResultDTO request)
        {
            try
            {
                var labResultId = await _labResultsService.RecordLabResultAsync(request);

                return labResultId==Guid.Empty ? BadRequest() : Ok(labResultId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong while Recording a Lab Result. Message {ErrorMessage}", ex.InnerException);
            }
            return BadRequest();
        }

        [HttpGet("{Id}")]
        public IActionResult GetLabResult(string Id)
        {
            return Ok(Id);
        }

        [HttpGet]
        public IActionResult GetLabResultByPatientId([FromQuery] string PatientId)
        {
            return Ok($"patient Id {PatientId}");
        }

        [HttpPut]
        public IActionResult UpdateLabResult(string DummyModel)
        {
            return Ok(DummyModel);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteLabResult(string Id)
        {
            return Ok(Id);
        }
    }
}

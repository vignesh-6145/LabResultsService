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
        public async Task<IActionResult> RecordLabResultsAsync([FromBody] RecordLabResultDTO request)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabResultAsync(string id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id,nameof(id));
            var record = await _labResultsService.GetLabResultByIdAsync(id);

            if (record is null){
                return NotFound();
            }

            return Ok(record);
        }

        [HttpGet]
        public async Task<IActionResult> GetLabResultByPatientIdAsync([FromQuery] string patientId, [FromQuery] bool includeDeletedRecords = false)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(patientId, nameof(patientId));
            var records = await _labResultsService.FilterLabResultsByPatientIdAsync(patientId, includeDeletedRecords);

            if (records is null)
            {
                return NotFound();
            }

            return Ok(records);
        }

        [HttpPut]
        public IActionResult UpdateLabResultAsync(string DummyModel)
        {
            return Ok(DummyModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabResultAsync(string id)
        {
            var recordUpdated = await _labResultsService.SoftDeleteLabResultAsync(id);

            return recordUpdated ? Ok() : BadRequest();
        }
    }
}

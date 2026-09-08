using LabResultsService.Core.Exceptions;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RecordLabResultsAsync([FromBody] RecordLabResultDTO request)
        {
            try
            {
                var labResultId = await _labResultsService.RecordLabResultAsync(request);

                return labResultId == Guid.Empty ? BadRequest() : Ok(labResultId);
            }
            catch (Exception aex) when (aex is ArgumentNullException || aex is InvalidDataException)
            {
                _logger.LogError(aex, "Invalid Data Found. Message {ErrorMessage}", aex.Message);
                return BadRequest(aex.Message);
            }
            catch (ResourceNotFoundException rex)
            {
                _logger.LogError(rex, "Couldn't find the required resource. ErrorMessage {ErrorMessage}", rex.Message);
                return NotFound(rex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLabResultAsync(string id)
        {
            try
            {
                var record = await _labResultsService.GetLabResultByIdAsync(id);

                if (record is null)
                {
                    return NoContent();
                }

                return Ok(record);
            }
            catch (Exception aex) when (aex is ArgumentNullException || aex is InvalidDataException)
            {
                _logger.LogError(aex, "Invalid Data Found. Message {ErrorMessage}", aex.Message);
                return BadRequest(aex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLabResultByPatientIdAsync([FromQuery] string patientId, [FromQuery] bool includeDeletedRecords = false)
        {
            try
            {
                var records = await _labResultsService.FilterLabResultsByPatientIdAsync(patientId, includeDeletedRecords);
                if (records is null)
                {
                    return NoContent();
                }

                return Ok(records);
            }
            catch (Exception aex) when (aex is ArgumentNullException || aex is InvalidDataException)
            {
                _logger.LogError(aex, "Invalid Data Found. Message {ErrorMessage}", aex.Message);
                return BadRequest(aex.Message);
            }
            catch (ResourceNotFoundException rex)
            {
                _logger.LogError(rex, "Couldn't find the required resource. ErrorMessage {ErrorMessage}", rex.Message);
                return NotFound(rex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLabResultAsync(string id, [FromBody] UpdateLabResultDTO request)
        {
            try
            {
                var recordUpdated = await _labResultsService.UpdateLabResult(id, request);
                //ArgumentNullException
                //InvalidDataException
                //RecordNotFoundException
                return recordUpdated ? Ok() : BadRequest();
            }
            catch (Exception aex) when (aex is ArgumentNullException || aex is InvalidDataException)
            {
                _logger.LogError(aex, "Invalid Data Found. Message {ErrorMessage}", aex.Message);
                return BadRequest(aex.Message);
            }
            catch (ResourceNotFoundException rex)
            {
                _logger.LogError(rex, "Couldn't find the required resource. ErrorMessage {ErrorMessage}", rex.Message);
                return NotFound(rex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabResultAsync(string id)
        {
            try
            {
                var recordUpdated = await _labResultsService.SoftDeleteLabResultAsync(id);
                //ArgumentNullException
                //InvalidDataException
                //RecordNotFoundException
                return recordUpdated ? Ok() : BadRequest();
            }
            catch (Exception aex) when (aex is ArgumentNullException || aex is InvalidDataException)
            {
                _logger.LogError(aex, "Invalid Data Found. Message {ErrorMessage}", aex.Message);
                return BadRequest(aex.Message);
            }
            catch (ResourceNotFoundException rex)
            {
                _logger.LogError(rex, "Couldn't find the required resource. ErrorMessage {ErrorMessage}", rex.Message);
                return NotFound(rex.Message);
            }
        }
    }
}

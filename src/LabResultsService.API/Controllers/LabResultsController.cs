using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabResultsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabResultsController : ControllerBase
    {
        [HttpPost]
        public IActionResult LabResults()
        {
            return Ok();
        }

        [HttpGet("{Id}")]
        public IActionResult GetLabResult(string Id)
        {
            return Ok(Id);
        }

        [HttpGet]
        public IActionResult GetLabResultByPatientId([FromQuery]string PatientId)
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

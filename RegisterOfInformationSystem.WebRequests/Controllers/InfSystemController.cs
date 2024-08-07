using Microsoft.AspNetCore.Mvc;
using RegisterOfInformationSystem.WebRequests.RequestContracts;
using RegisterOfInformationSystems.Service;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfSystemController(InformationSystemService infSystemService) : ControllerBase
    {
        private readonly InformationSystemService _infSystemService = infSystemService;
        [HttpPost("create")]
        public async Task<IActionResult> AddNewInfSystem([FromBody] CreateInfSystem request)
        {
            if (await _infSystemService.CreateInformationSystem(request.Name, request.Description, request.AttributeKeys, request.AttributeValues))
            {
                return Ok("Created");
            }
            return Ok();
        }
        [HttpGet("getsystems")]
        public async Task<IActionResult> GetInfSystem([FromQuery] GetInfSystemRequests request)
        {
            var systems = _infSystemService.GetInformationSystemsByPartOfName(request.Search);
            if(systems != null)
            {
                return Ok(systems.ToList());
            }
            return Ok("No Information Systems");
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteInfSystem([FromBody] DeleteInfSystem request)
        {
            if(await _infSystemService.DeleteInformationSystem(request.Name))
            {
                return Ok("Deleted");
            }
            return Ok("Information System does not exist");
        }
        [HttpPut("change")]
        public async Task<IActionResult> SetNewAttributes([FromBody]ChangeAttributes request)
        {
            _infSystemService.UpdateInformationSystem(request.Name, request.AttrNames, request.AttrValues);
            return Ok(true);
        }
    }
}

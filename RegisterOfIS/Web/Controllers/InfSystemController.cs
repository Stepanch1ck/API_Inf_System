using Microsoft.AspNetCore.Mvc;
using RegisterOfIS.Requests;
using RegisterOfIS.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace RegisterOfIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InfSystemController(IInfSystemService infSystemService) : ControllerBase
    {
        private readonly IInfSystemService _infSystemService = infSystemService;
        [HttpPost("create")]
        public async Task<IActionResult> AddNewInfSystem([FromBody] CreateInfSystem request)
        {
            if (await _infSystemService.CreateInformationSystem(request.Name, request.Description, request.AttributeKeys, request.AttributeValues))
            {
                return Ok("Created");
            }
            return Ok("Something went wrong");
        }
        [HttpGet("getsystems")]
        public async Task<IActionResult> GetInfSystem([FromQuery] GetInfSystemRequests request)
        {
            var systems = await _infSystemService.GetInformationSystemsByPartOfName(request.Search);
            if(systems != null)
            {
                return Ok(systems.ToList());
            }
            return Ok("No Information Systems");
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteInfSystem([FromQuery] string infSystemName)
        {
            if(await _infSystemService.DeleteInformationSystem(infSystemName))
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

        [HttpGet("GetISWithAttribute")]
        public async Task<IActionResult> GetISByAttributes([FromQuery] GetInfSystemWithAttribute request)
        {
            var systems = _infSystemService.GetInformationSystemsByAttributeContain(request.AttrName);
            if (systems == null)
            {
                return Ok("No IS with this attribute");
            }
            return Ok(systems);
        }
    }
}

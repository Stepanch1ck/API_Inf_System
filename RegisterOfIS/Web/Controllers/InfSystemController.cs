using Microsoft.AspNetCore.Mvc;
using RegisterOfIS.Requests;
using RegisterOfIS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;
using RegisterOfIS.Models;

namespace RegisterOfIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InfSystemController(IInfSystemService infSystemService) : ControllerBase
    {
        private readonly IInfSystemService _infSystemService = infSystemService;

        [HttpGet]
        public async Task<ActionResult<List<InfSystem>>> SearchInfSystems([FromQuery][AllowNull] SearchInfSystemsForm form)
        {
            return Ok(await _infSystemService.Search(form));
        }

        [HttpGet("{id}")]
        public ActionResult<InfSystem> GetInfSystemById([FromRoute] int id)
        {
            return Ok(_infSystemService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<InfSystem>> CreateInfSystem([FromBody] CreateInfSystemRequest request)
        {
            return Ok(await _infSystemService.Create(request));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InfSystem>> UpdateInfSystem([FromRoute] int id, [FromBody] ChangeInfSystemRequest request)
        {
            return Ok(await _infSystemService.Update(id, request));
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteInfSystem([FromRoute] int id)
        {
            _infSystemService.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}/Attrs")]
        public ActionResult<List<AttributeStateDto>> GetAttrs([FromRoute] int id)
        {
            return Ok(_infSystemService.GetAttrs(id));
        }

        [HttpPut("{id}/Attrs")]
        public async Task<IActionResult> AddAttrs([FromRoute] int id, [FromBody] Dictionary<int, string> data)
        {
            return Ok(await _infSystemService.AddAttrs(id, data));
        }

        [HttpDelete("{id}/Attrs")]
        public async Task<IActionResult> RemoveAttrs([FromRoute] int id, [FromBody] List<int> data)
        {
            return Ok(await _infSystemService.RemoveAttrs(id, data));
        }
    }
}

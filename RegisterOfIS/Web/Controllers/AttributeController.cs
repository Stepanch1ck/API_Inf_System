using Microsoft.AspNetCore.Mvc;
using RegisterOfIS.Requests;
using RegisterOfIS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Attribute = RegisterOfIS.Models.Attribute;
using System.Diagnostics.CodeAnalysis;

namespace RegisterOfIS.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AttributeController(IAttributeService service) : ControllerBase
    {
        private readonly IAttributeService _attributeService = service;


        [HttpGet]
        public async Task<ActionResult<List<Attribute>>> SearchAttributes([FromQuery] [AllowNull] SearchAtrributesForm form)
        {
            return Ok(await _attributeService.Search(form));
        }

        [HttpGet("{id}")]
        public ActionResult<Attribute> GetAttributeById([FromRoute] int id)
        {
            return Ok(_attributeService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<Attribute>> CreateAttribute([FromBody] CreateAttributeRequest request)
        {
            return Ok(await _attributeService.Create(request));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Attribute>> UpdateAttribute([FromRoute] int id, [FromBody] ChangeAttributeRequest request)
        {
            return Ok(await _attributeService.Update(id, request));
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteAttribute([FromRoute] int id)
        {
            _attributeService.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}/Dict")]
        public ActionResult<List<string>> GetDict([FromRoute] int id)
        {
            return Ok(_attributeService.GetDict(id));
        }

        [HttpPut("{id}/Dict")]
        public async Task<IActionResult> AddDict([FromRoute] int id, [FromBody] List<string> data)
        {
            return Ok(await _attributeService.AddDict(id, data));
        }

        [HttpDelete("{id}/Dict")]
        public async Task<IActionResult> RemoveDict([FromRoute] int id, [FromBody] List<string> data)
        {
            return Ok(await _attributeService.RemoveDict(id, data));
        }
    }
}

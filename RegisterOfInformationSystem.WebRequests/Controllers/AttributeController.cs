using Microsoft.AspNetCore.Mvc;
using RegisterOfInformationSystem.WebRequests.RequestContracts;
using Services;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributeController(AttributeService service) : ControllerBase
    {
        private readonly AttributeService _attributeService = service;
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewAttribute([FromBody] CreateAttributeRequest request)
        {
            if(await _attributeService.CreateAttribute(request.Name, request.Description, request.States))
            {
                return Ok("created");
            }
            return Ok("Attribute with this name already exists");
        }
        [HttpGet("getattributes")]
        public async Task<IActionResult> GetAllAttributes()
        {
            var attrs = _attributeService.GetAttributesByPartOfName("");
            if (attrs == null)
            {
                return Ok("No attributes exist");
            }
            return Ok(attrs);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAttribute([FromBody] DeleteAttribute request)
        {
            if(await _attributeService.DeleteAttribute(request.Name))
            {
                return Ok("Deleted");
            }
            return Ok("Attribute with this name does not exist");
        }
    }
}

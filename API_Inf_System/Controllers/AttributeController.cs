using API_Inf_System.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Inf_System.DTO;
using API_Inf_System.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using API_Inf_System.DBContext;
using API_Inf_System.Services;

namespace API_Inf_System.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AttributeDto>>> GetAllAttributes()
        {
            return await _attributeService.GetAllAttributesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeDto>> GetAttribute(int id)
        {
            var attribute = await _attributeService.GetAttributeByIdAsync(id);

            if (attribute == null)
            {
                return NotFound();
            }

            return attribute;
        }

        [Authorize(Roles = "admin, editor")]
        [HttpPost]
        public async Task<ActionResult<AttributeCreateDto>> PostAttribute(AttributeCreateDto attributeDto)
        {
            var newAttribute = await _attributeService.CreateAttributeAsync(attributeDto);
            return CreatedAtAction(nameof(GetAttribute), new { id = newAttribute.Id }, newAttribute);
        }
        
        [Authorize(Roles = "admin, editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttribute(int id, AttributeDto attributeDto)
        {
            if (id != attributeDto.Id)
            {
                return BadRequest();
            }

            var isUpdated = await _attributeService.UpdateAttributeAsync(id, attributeDto);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            var isDeleted = await _attributeService.DeleteAttributeAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

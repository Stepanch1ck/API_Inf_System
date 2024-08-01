using Microsoft.AspNetCore.Mvc;
using API_Inf_System.DBContext;
using API_Inf_System.Models;
using API_Inf_System.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API_Inf_System.Models.DTO;

namespace API_Inf_System.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeValuesController : ControllerBase
    {
        private readonly IAttributeValueService _attributeValueService;

        public AttributeValuesController(IAttributeValueService attributeValueService)
        {
            _attributeValueService = attributeValueService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AttributeValueDto>>> GetAllAttributeValues()
        {
            return await _attributeValueService.GetAllAttributeValuesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeValue>> GetAttributeValue(int id)
        {
            var attributeValue = await _attributeValueService.GetAttributeValueByIdAsync(id);

            if (attributeValue == null)
            {
                return NotFound();
            }

            return attributeValue;
        }

        [Authorize(Roles = "admin, editor")]
        [HttpPost]
        public async Task<ActionResult<AttributeValue>> CreateAttributeValue(AttributeValue attributeValue)
        {
            var newAttributeValue = await _attributeValueService.CreateAttributeValueAsync(
                attributeValue.InformationSystemId,
                attributeValue.AttributeId,
                attributeValue.Value);

            return CreatedAtAction(nameof(GetAttributeValue), new { id = newAttributeValue.Id }, newAttributeValue);
        }

        [Authorize(Roles = "admin, editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttributeValue(int id, AttributeValue attributeValue)
        {
            if (id != attributeValue.Id)
            {
                return BadRequest();
            }

            var isUpdated = await _attributeValueService.UpdateAttributeValueAsync(
                id,
                attributeValue.InformationSystemId,
                attributeValue.AttributeId,
                attributeValue.Value);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeValue(int id)
        {
            var isDeleted = await _attributeValueService.DeleteAttributeValueAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}

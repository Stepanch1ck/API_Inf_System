using API_Inf_System.DBContext;
using API_Inf_System.DTO;
using API_Inf_System.IServices;
using API_Inf_System.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Inf_System.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InformationSystemsController : ControllerBase
    {
        private readonly IInformationSystemService _informationSystemService;

        public InformationSystemsController(IInformationSystemService informationSystemService)
        {
            _informationSystemService = informationSystemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformationSystemDto>>> GetInformationSystems()
        {
            var informationSystems = await _informationSystemService.GetAllInformationSystemsAsync();
            return Ok(informationSystems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InformationSystemDto>> GetInformationSystem(int id)
        {
            var informationSystem = await _informationSystemService.GetInformationSystemByIdAsync(id);

            if (informationSystem == null)
            {
                return NotFound();
            }

            return informationSystem;
        }

        [Authorize(Roles = "admin, editor")]
        [HttpPost]
        public async Task<ActionResult<InformationSystem>> PostInformationSystem(InformationSystemCreateDto informationSystemDto)
        {
            var newInformationSystem = await _informationSystemService.CreateInformationSystemAsync(informationSystemDto);

            return CreatedAtAction(nameof(GetInformationSystem), new { id = newInformationSystem.Id }, newInformationSystem);
        }


        [Authorize(Roles = "admin, editor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformationSystem(int id, InformationSystemUpdateDto informationSystemDto)
        {
            if (id != informationSystemDto.Id)
            {
                return BadRequest();
            }

            var isUpdated = await _informationSystemService.UpdateInformationSystemAsync(id, informationSystemDto);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin, editor")]
        [HttpPut("{id}/attributes")]
        public async Task<IActionResult> PutInformationSystemAttributes(int id, InformationSystemAttributeUpdateDto informationSystemDto)
        {
            //if (id != informationSystemDto.Id)
            //{
            //    return BadRequest();
            //}

            var isUpdated = await _informationSystemService.UpdateInformationSystemAttributesAsync(id, informationSystemDto);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformationSystem(int id)
        {
            var isDeleted = await _informationSystemService.DeleteInformationSystemAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<InformationSystemDetailsDto>> GetInformationSystemDetails(int id)
        {
            var details = await _informationSystemService.GetInformationSystemDetailsAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            return details;
        }

        [Authorize(Roles = "admin, editor")]
        [HttpPut("{id}/edit")]
        public async Task<IActionResult> EditInformationSystem(int id, InformationSystemEditDto informationSystemDto)
        {
            if (await _informationSystemService.EditInformationSystemAsync(id, informationSystemDto))
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("with-attributes")]
        public async Task<ActionResult<List<InformationSystemWithAttributesDto>>> GetAllInformationSystemsWithAttributes()
        {
            return await _informationSystemService.GetAllInformationSystemsWithAttributesAsync();
        }

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public async Task<ActionResult<InformationSystem>> CreateInformationSystemWithAttributes(InformationSystemCreateDto informationSystemDto)
        //{
        //    var newInformationSystem = await _informationSystemService.CreateInformationSystemWithAttributesAsync(informationSystemDto);
        //    return CreatedAtAction(nameof(GetInformationSystem), newInformationSystem);
        //}

        [Authorize(Roles = "admin, editor")]
        [HttpPut("{id}/edit-with-attributes")]
        public async Task<IActionResult> EditInformationSystemWithAttributes(int id, InformationSystemEditDto informationSystemDto)
        {
            if (await _informationSystemService.EditInformationSystemWithAttributesAsync(id, informationSystemDto))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}

using InfSystemsAPI.Contracts;
using InfSystemsAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributeController(InfSystemAPIDbContext dbContext) : ControllerBase
    {
        private readonly InfSystemAPIDbContext _dbContext = dbContext;
        [HttpPost("create")]
        public async Task<IActionResult> CreateNewAttribute([FromBody] CreateAttributeRequest request)
        {
            if (!_dbContext.Attributes.Where(attr => attr.Name.ToLower() == request.Name.ToLower()).Any())
            {
                await _dbContext.Attributes.AddAsync(new Models.Attribute(request.Name, request.Description, request.States));
                await _dbContext.SaveChangesAsync();
                return Ok("created");
            }
            else if(_dbContext.Attributes.Where(attr => attr.Name.ToLower() == request.Name.ToLower()).Any())
            {
                return Ok("uzhe est");
            }
            return BadRequest();
        }
        [HttpGet("getattributes")]
        public async Task<IActionResult> GetAllAttributes([FromQuery]GetAttributes request)
        {
            if(request.Search == null)
            {
                return Ok(_dbContext.Attributes.Where(attr => true));
            }
            return Ok(_dbContext.Attributes.Where(attr => attr.Name.ToLower().Contains(request.Search.ToLower())));
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAttribute([FromBody]DeleteAttribute request)
        {
            if (_dbContext.Attributes.Where(attr => attr.Name.ToLower() == request.Name.ToLower()).Any())
            {
                _dbContext.Remove(_dbContext.Attributes.Where(attr => attr.Name.ToLower() == request.Name.ToLower()).ToArray()[0]);
                await _dbContext.SaveChangesAsync();
                return Ok("deleted");
            }
            else
            {
                return Ok("Attribute does not exist");
            }
        }
    }
}

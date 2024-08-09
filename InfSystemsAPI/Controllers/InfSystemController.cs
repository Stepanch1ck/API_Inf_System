using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfSystemsAPI.Contracts;
using InfSystemsAPI.DataAccess;
using InfSystemsAPI.Models;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfSystemController(InfSystemAPIDbContext dbContext) : ControllerBase
    {
        private readonly InfSystemAPIDbContext _dbContext = dbContext;
        [HttpPost("create")]
        public async Task<IActionResult> AddNewInfSystem([FromBody]CreateInfSystem request)
        {
            if(!_dbContext.InfSystems.Where(sys => sys.Name.Equals(request.Name)).Any())
            {
                var tempDict = new Dictionary<string, string>();
                for(int i=0; i<request.AttributeKeys.Length;i++)
                {
                    tempDict.Add(request.AttributeKeys[i], request.AttributeValues[i]);
                }
                var infSys = new InfSystem(request.Name, request.Description, tempDict);
                await _dbContext.InfSystems.AddAsync(infSys);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("uzhe est pohodu");
        }
        [HttpGet("getsystems")]
        public async Task<IActionResult> GetInfSystem([FromQuery]GetInfSystemRequests request)
        {
            var InfSysQuery = _dbContext.InfSystems
                .Where(sys => 
                string.IsNullOrWhiteSpace(request.Search) 
                || sys.Name.Contains(request.Search));
            return Ok(await InfSysQuery?.ToListAsync());
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteInfSystem([FromBody]DeleteInfSystem request)
        {
            var system = _dbContext.InfSystems.Where(sys => sys.Name.Equals(request.Name)).ToArray()[0];
            if (system != null)
            {
                _dbContext.InfSystems.Remove(system);
                await _dbContext.SaveChangesAsync();
                return Ok("vse good");
            }
            return BadRequest("shoto ne to");
        }
    }
}

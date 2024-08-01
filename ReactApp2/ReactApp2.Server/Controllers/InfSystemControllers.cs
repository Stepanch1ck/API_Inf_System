using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp2.Server.Contracts;
using ReactApp2.Server.DataAccess;
using ReactApp2.Server.Models;

namespace ReactApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfSystemControllers(InfSystemDbContext dbContext) : ControllerBase
    {
        private readonly InfSystemDbContext _dbContext = dbContext;
        [HttpPost]
        public async Task<IActionResult> AddNewInfSystem([FromBody]CreateInfSystem request)
        {
            var infSys = new InfSystem(request.Name, request.Description );
            await _dbContext.InfSystems.AddAsync(infSys);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetInfSystem([FromQuery]GetInfSystemRequests request)
        {
            var InfSysQuery = _dbContext.InfSystems
                .Where(sys => 
                string.IsNullOrWhiteSpace(request.Search) 
                || sys.Name.ToLower()
                           .Contains(request.Search.ToLower()));
            return Ok(await InfSysQuery?.ToListAsync());
        }
    }
}

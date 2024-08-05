using InfSystemsAPI.Contracts;
using InfSystemsAPI.DataAccess;
using InfSystemsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController(InfSystemAPIDbContext dbContext) : ControllerBase
    {
        private readonly InfSystemAPIDbContext _dbContext = dbContext;
        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody]RoleCreatingRequest request)
        {
            if (_dbContext.Roles.Where(role => role.Name.ToLower() == request.Name.ToLower()).Count() == 0)
            {
                await _dbContext.Roles.AddAsync(new Role(request.Power, request.Name, request.Description));
                await _dbContext.SaveChangesAsync();
                return Ok("Role created");
            }
            return Ok("Beda");
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole([FromBody]RoleDeletingRequest request)
        {
            if(_dbContext.Roles.Where(role => role.Name.ToLower() == request.Name.ToLower()).Count()>0)
            {
                _dbContext.Roles.Remove(_dbContext.Roles.Where(role => role.Name.ToLower() == request.Name.ToLower()).ToArray()[0]);
                await _dbContext.SaveChangesAsync();
                return Ok("role deleted");
            }

            return Ok("ne vishlo");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(string.Join(' ',_dbContext.Roles.Select(role => role.Name)));
        }
    }
}

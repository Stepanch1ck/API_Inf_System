using InfSystemsAPI.Contracts;
using InfSystemsAPI.DataAccess;
using InfSystemsAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class LoginController(InfSystemAPIDbContext dbContext) : ControllerBase
    {
        private readonly InfSystemAPIDbContext _dbContext = dbContext;
        [HttpGet("signin")]
        public async Task<IActionResult> IsUserValid([FromQuery]UserValidation request)
        {
            if (_dbContext.Users?.Where(user => user.Login.Equals(request.Username)).Count() != 0)
            {
                if(request.Password.GetHashCode() == _dbContext.Users?.Where(user => user.Login.Equals(request.Username)).ToArray()[0].Password)
                {
                    return Ok("true");
                }
                return Ok("Wrong password");
            }
            else if (_dbContext.Users?.Where(user => user.Login == request.Username).Count() == 0)
            {
                return Ok("No users try to register");
            }
            else { return BadRequest(); }
        }
    }
}

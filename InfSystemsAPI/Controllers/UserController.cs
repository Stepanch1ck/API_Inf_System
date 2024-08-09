using InfSystemsAPI.Contracts;
using InfSystemsAPI.DataAccess;
using InfSystemsAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(InfSystemAPIDbContext dbContext) : ControllerBase
    {
        private readonly InfSystemAPIDbContext _dbContext = dbContext;
        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistration request)
        {
            if (_dbContext.Users.Where(user => user.Login == request.Username).Any())
            {
                var user = new User(request.Username, request.Password);
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return Ok("User created");
            }
            Console.WriteLine();
            return Ok("We have problems");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveUserFromDB([FromBody] UserRemoving request)
        {
            if (_dbContext.Users.Where(user => user.Login == request.Username).Count() == 1)
            {
                _dbContext.Users.Remove(_dbContext.Users.Where(user => user.Login.Equals(request.Username)).ToArray()[0]);
                await _dbContext.SaveChangesAsync();
                return Ok("Deleted");
            }
            return Ok("I have a bad news");
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsernames()
        {
            return Ok(string.Join(' ', _dbContext.Users.Where(e => true).Select(user => user.Login)));
        }
    }
}

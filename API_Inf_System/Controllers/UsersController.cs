using API_Inf_System.DBContext;
using API_Inf_System.IServices;
using API_Inf_System.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Inf_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);

            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> CreateUser(UserDto userDto)
        {
            var newUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _userService.DeleteUserAsync(id))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")] 
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            if (await _userService.UpdateUserAsync(id, userDto))
            {
                return NoContent();
            }

            return NotFound();
        }
    }

}

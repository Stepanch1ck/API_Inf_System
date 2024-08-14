using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterOfIS.Requests;
using RegisterOfIS.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace RegisterOfIS.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService, IRoleService roleService, IHttpContextAccessor accessor) : ControllerBase
    {
        IHttpContextAccessor Accessor = accessor;
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;
        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistration request)
        {
            if (await _userService.CreateUser(request.Username, request.Password, request.Role))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveUserFromDB([FromQuery] string userName)
        {
            if (await _userService.DeleteUser(userName))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("getuserbylogin")]
        public async Task<IActionResult> GetUsernamesByLogin([FromQuery] string userName)
        {
            if((_userService.GetByLogin(userName)) != null)
            {
                return Ok(_userService.GetByLogin(userName));
            }
            return Ok("User does not exist");
        }
        [HttpGet("getuserbyrole")]
        public async Task<IActionResult> GetUsernamesByRole([FromQuery] string role)
        {
            if ((await _userService.GetByRole(role)) != null)
            {
                return Ok(await _userService.GetByRole(role));
            }
            return Ok("User does not exist");
        }
        [HttpGet("getroles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(_roleService.GetAllRoles());
        }
        [HttpPut("changeUser")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUser request)
        {
            if (await _userService.ChangeUser(request.Login, request.Password, request.Role))
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [HttpGet("allusers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request)
        {
            return Ok(await _userService.GetAllUsers());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterOfInformationSystem.WebRequests.RequestContracts;
using Services;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(UserService userService, IHttpContextAccessor accessor) : ControllerBase
    {
        IHttpContextAccessor Accessor = accessor;
        private readonly UserService _userService = userService;
        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistration request)
        {
            HttpContext context = this.Accessor.HttpContext;
            if (context.Request.Cookies["role"] != "admin" )
            {
                return Ok(false);
            }
            if (await _userService.CreateUser(request.Username, request.Password, request.Role))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveUserFromDB([FromBody] UserRemoving request)
        {
            if (await _userService.DeleteUser(request.Username))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("getuserbylogin")]
        public async Task<IActionResult> GetUsernamesByLogin([FromQuery]GetUserByUsername request)
        {
            if((_userService.GetByLogin(request.Username)) != null)
            {
                return Ok(_userService.GetByLogin(request.Username));
            }
            return Ok("User does not exist");
        }
        [HttpGet("getuserbyrole")]
        public async Task<IActionResult> GetUsernamesByRole([FromQuery] GetUserByRole request)
        {
            if ((_userService.GetByRole(request.Role)) != null)
            {
                return Ok(_userService.GetByRole(request.Role));
            }
            return Ok("User does not exist");
        }
        [HttpGet("getroles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(_userService.GetAllRoles());
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePawword request)
        {
            if (await _userService.ChangePassword(request.Login, request.Password))
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}

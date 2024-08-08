using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterOfInformationSystem.WebRequests.RequestContracts;
using Services;

namespace InfSystemsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IHttpContextAccessor Accessor;
        private readonly UserService _userService;
        public LoginController(IHttpContextAccessor accessor, UserService userService)
        {
            this.Accessor = accessor;
            this._userService = userService;
        }

        [HttpGet("signin")]
        public async Task<IActionResult> IsUserValid([FromQuery] UserValidation request)
        {
            var response = false;
            HttpContext context = this.Accessor.HttpContext;
            if (_userService.LogInPermission(request.Username, request.Password))
            {
                context.Response.Cookies.Append("role", (_userService.GetByLogin(request.Username)).Role.Name);
                response = true;
                return Ok(response);
            }
            return Ok(response);
        }
    }
}

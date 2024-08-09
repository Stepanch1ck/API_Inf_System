using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterOfIS.Web.Security;
using RegisterOfIS.Web.Requests;

namespace RegisterOfIS.Web.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly BasicAuthService _basicAuthService;

        public AuthController(ILogger<AuthController> logger,
                                BasicAuthService basicAuthService,
                                JwtAuthService jwtAuthService)
        {
            _logger = logger;
            _basicAuthService = basicAuthService;
        }


        
        [HttpPost("signIn")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _basicAuthService.SignIn(request.UserName, request.Password);

            if (!result.Success) return Unauthorized();

            _logger.LogInformation($"User [{request.UserName}] logged in the system.");

            return Ok(new LoginResponse(result.User.Login, result.AccessToken, result.RefreshToken));
        }


        [HttpPost("refreshtoken")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = await _basicAuthService.RefreshToken(request.AccessToken, request.RefreshToken);

            if (!result.Success) return Unauthorized();

            return Ok(new LoginResponse(result.User.Login, result.AccessToken, result.RefreshToken));
        }

    }
}

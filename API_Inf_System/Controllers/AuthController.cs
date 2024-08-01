using Microsoft.AspNetCore.Mvc;
using API_Inf_System.DBContext;
using API_Inf_System.Models;
using API_Inf_System.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API_Inf_System.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly MyDbContext _context;

        public AuthController(IUserService userService, IJwtService jwtService, MyDbContext context)
        {
            _userService = userService;
            _jwtService = jwtService;
            _context = context; 
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            var user = await _userService.AuthenticateAsync(request.Username, request.Password);

            if (user == null)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

                if (existingUser == null)
                {
                    return Unauthorized("Пользователь с таким именем не найден.");
                }
                else
                {
                    return Unauthorized("Неверный пароль.");
                }
            }
            

            var token = _jwtService.GenerateToken(user);

            return Ok(new { Token = token });
        }
    }
}

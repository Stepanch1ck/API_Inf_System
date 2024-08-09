using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RegisterOfIS.Models;

using RegisterOfIS.Database;
using RegisterOfIS.Web.Security;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;

namespace RegisterOfIS.Web.Security
{
    public class BasicAuthService
    {
        private readonly ILogger<BasicAuthService> _logger;
        private readonly PostgresDbContext _ctx;
        private readonly JwtAuthService _jwtAuthService;
        private readonly JwtTokenOptions _jwtTokenOptions;

        public BasicAuthService(ILogger<BasicAuthService> logger,
                             JwtAuthService jwtAuthService,
                             IOptions<JwtTokenOptions> jwtTokenOptions,
                             PostgresDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
            _jwtAuthService = jwtAuthService;
            _jwtTokenOptions = jwtTokenOptions.Value;
        }

        public async Task<SignInResult> SignIn(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]", userName);

            SignInResult result = new SignInResult();

            if (string.IsNullOrWhiteSpace(userName)) return result;
            if (string.IsNullOrWhiteSpace(password)) return result;

            var user = await _ctx.Users.Include(u => u.Role).Where(f => f.Login == userName).FirstOrDefaultAsync();



            if (user != null && PasswordService.VerifyPassword(password, user.Password))
            {

                var claims = BuildClaims(user);
                result.User = user;
                result.AccessToken = _jwtAuthService.BuildToken(claims);
                result.RefreshToken = _jwtAuthService.BuildRefreshToken();

                _ctx.RefreshTokens.Add(new RefreshToken { UserName = user.Login, Token = result.RefreshToken, IssuedAt = DateTime.Now.ToUniversalTime(), ExpiresAt = DateTime.Now.AddMinutes(_jwtTokenOptions.RefreshTokenExpiration).ToUniversalTime() });
                _ctx.SaveChanges();

                result.Success = true;
            };

            return result;
        }

        public async Task<SignInResult> RefreshToken(string AccessToken, string RefreshToken)
        {

            ClaimsPrincipal claimsPrincipal = _jwtAuthService.GetPrincipalFromToken(AccessToken);
            SignInResult result = new SignInResult();

            if (claimsPrincipal == null) return result;

            string id = claimsPrincipal.Claims.First(c => c.Type == "id").Value;
            var user = await _ctx.Users.FindAsync(Convert.ToInt32(id));

            if (user == null) return result;

            var token = await _ctx.RefreshTokens
                    .Where(f => f.UserName == user.Login
                            && f.Token == RefreshToken
                            && f.ExpiresAt >= DateTime.Now)
                    .FirstOrDefaultAsync();

            if (token == null) return result;

            var claims = BuildClaims(user);

            result.User = user;
            result.AccessToken = _jwtAuthService.BuildToken(claims);
            result.RefreshToken = _jwtAuthService.BuildRefreshToken();

            _ctx.RefreshTokens.Remove(token);
            _ctx.RefreshTokens.Add(new RefreshToken { UserName = user.Login, Token = result.RefreshToken, IssuedAt = DateTime.Now.ToUniversalTime(), ExpiresAt = DateTime.Now.AddMinutes(_jwtTokenOptions.RefreshTokenExpiration).ToUniversalTime() });
            _ctx.SaveChanges();

            result.Success = true;

            return result;
        }

        private Claim[] BuildClaims(User user)
        {
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            return claims;
        }

    }

}

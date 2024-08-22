using System.ComponentModel.DataAnnotations;

namespace RegistryOfInformationSystems.Contracts
{
  
    public record LoginRequest([Required] string UserName, [Required] string Password);
    public record LoginResponse(string UserName, string role, string AccessToken, string RefreshToken);
    public record RefreshTokenRequest(string AccessToken, string RefreshToken);

}

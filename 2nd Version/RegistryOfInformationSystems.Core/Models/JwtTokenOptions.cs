namespace RegistryOfInformationSystems.Models
{
    public class JwtTokenOptions
    {
        public const string Jwt = "Jwt";

        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }

    }
}

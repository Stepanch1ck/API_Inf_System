using RegisterOfIS.Models;

namespace RegisterOfIS.Web.Security
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public User User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public SignInResult()
        {
            Success = false;
        }
    }
}

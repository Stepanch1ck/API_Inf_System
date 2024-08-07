using System.Security.Cryptography;
using System.Text;

namespace RegisterOfInformationSystems.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        public Role? Role { get; set; }
        public Guid RoleId { get; set; }
        public User(string login, string password, Role role)
        {
            this.Login = login;
            this.Password = GetSHA256(password);
            this.Role = role;
        }
        public User() { }
        private static string GetSHA256(string password)
        {
            var byteArray = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(byteArray);
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace RegisterOfInformationSystems.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int RoleId { get; set; } = 3;
        public Role? Role { get; set; }
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

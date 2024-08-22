using Newtonsoft.Json;

namespace RegistryOfInformationSystems.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public User(string login, string password, Role role)
        {
            this.Login = login;
            this.Password = password;
            this.Role = role;
        }
        public User() { }
    }
}

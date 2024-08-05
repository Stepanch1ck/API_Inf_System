namespace InfSystemsAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public int Password { get; set; }
        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password.GetHashCode();
        }
        public User() { }
    }
}

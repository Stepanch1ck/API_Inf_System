namespace API_Inf_System.Models.DTO
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}

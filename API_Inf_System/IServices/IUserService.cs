using API_Inf_System.DBContext;

namespace API_Inf_System.IServices
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userid);
    }
}

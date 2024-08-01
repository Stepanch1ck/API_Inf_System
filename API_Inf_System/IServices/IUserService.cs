using API_Inf_System.DBContext;
using API_Inf_System.Models.DTO;

namespace API_Inf_System.IServices
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userid);
        Task<User> CreateUserAsync(UserCreateDto userDto);
        Task<bool> UpdateUserAsync(int userId, UserUpdateDto userDto); 
        Task<bool> DeleteUserAsync(int userId);
    }
}

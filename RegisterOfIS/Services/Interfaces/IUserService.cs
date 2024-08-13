 using RegisterOfIS.Models;

namespace RegisterOfIS.Interfaces
{
    public interface IUserService
    {
        User? GetByLogin(string login);
        Task<List<User>?> GetByRole(string role);
        Task<bool> CreateUser(string login, string password, string role);
        Task<bool> DeleteUser(string login);
        Task<bool> ChangePassword(string login, string newPassword);
        List<User> GetAllUsers();
    }
}

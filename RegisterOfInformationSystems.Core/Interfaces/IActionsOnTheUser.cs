 using RegisterOfInformationSystems.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Interfaces
{
    public interface IActionsOnTheUser
    {
        User? GetByLogin(string login);
        List<User>? GetByRole(string role);
        bool LogInPermission(string login, string password);
        Task<bool> CreateUser(string login, string password, int rolePower);
        Task<bool> DeleteUser(string login);
        Task<bool> ChangePassword(string login, string newPassword);
    }
}

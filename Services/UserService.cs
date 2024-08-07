using Microsoft.EntityFrameworkCore;
using RegisterOfInformationSystems.Core.Interfaces;
using RegisterOfInformationSystems.Core.Models;
using RegisterOfInformationSystems.Database;
using RegisterOfInformationSystems.Service;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService(PostgresDbContext dbContext) : IActionsOnTheUser
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        private readonly RoleService _roleService = new(dbContext);

        public async Task<bool> ChangePassword(string login, string newPassword)
        {
            #region НайтиПользователя
            var user = GetByLogin(login);
            #endregion

            #region false если нет такого
            if (user == null) return false;
            #endregion

            #region Удалить Пользователя
            #endregion

            #region Создать Пользователя
            await CreateUser(login, newPassword, user.Role.RolePower);
            #endregion 

            return true;
        }

        public async Task<bool> CreateUser(string login, string password, int rolePower)
        {
            #region НайтиПользователя
            var check = GetByLogin(login);
            #endregion

            #region false если уже существует
            if (check != null) return false;
            #endregion

            #region создать пользователя и закинуть его в бд
            var tempUser = new User(login, password, _roleService.GetRolePowerOf(rolePower));
            await _dbContext.Users.AddAsync(tempUser);
            await _dbContext.SaveChangesAsync();
            #endregion

            return true;
        }

        public async Task<bool> DeleteUser(string login)
        {
            #region НайтиПользователя
            var user = GetByLogin(login);
            #endregion

            #region false если нет такого
            if (user == null) return false;
            #endregion

            #region добавить в бд
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            #endregion

            return true;
        }

        public User? GetByLogin(string login)
        {
            #region Найти по логину и вернуть его
            return _dbContext.Users.Include(user => user.Role).Where(user => user.Login.ToLower() == login.ToLower()).FirstOrDefault();
            #endregion
        }

        public async Task<List<User>?> GetByRole(string name)
        {
            #region найти роль по названию и пользователей с этой ролью
            var role = _roleService.GetRoleByName(name);
            var user = _dbContext.Users.Where(user => user.Role.Name.Equals(name));
            #endregion

            #region вернуть null если таких нет
            if (user.FirstOrDefault() == null) return null;
            #endregion

            #region вернуть всех пользователей
            return [.. user];
            #endregion
        }

        public async Task<bool> LogInPermission(string login, string password)
        {
            #region Найти пользователя по логину
            var user = GetByLogin(login);
            #endregion

            #region Вернуть запрет на авторизацию если нет пользователя
            if (user == null) return false;
            #endregion

            #region Сравнение пароля
            if (await GetSHA256(password) == user.Password) return true;
            #endregion

            #region Вернуть запрет на авторизацию если неверный пароль 
            return false;
            #endregion
        }
        private static async Task<string> GetSHA256(string password)
        {
            #region Создать и вернуть хеш пароля в SHA256
            var byteArray = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(byteArray);
            #endregion
        }
        public async Task<List<Role>> GetAllRoles()
        {
            #region Вернуть список всех ролей
            return await _roleService.GetAllRoles();
            #endregion
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RegisterOfIS.Interfaces;
using RegisterOfIS.Models;
using RegisterOfIS.Database;
using RegisterOfIS.Web.Security;

namespace RegisterOfIS.Services
{
    public class UserService(PostgresDbContext dbContext) : IUserService
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        private readonly RoleService _roleService = new(dbContext);

        public async Task<bool> ChangeUser(string login, string newPassword, string role)
        {
            #region НайтиПользователя
            var user = GetByLogin(login);
            #endregion

            #region false если нет такого
            if (user == null) return false;
            #endregion

            #region Удалить Пользователя
            user.Password = PasswordService.HashPassword(newPassword);
            user.Role = _roleService.GetRoleByName(role);
            await _dbContext.SaveChangesAsync();
            #endregion

            return true;
        }

        public async Task<bool> CreateUser(string login, string password, string role)
        {
            #region НайтиПользователя
            var check = GetByLogin(login);
            #endregion

            #region false если уже существует
            if (check != null) return false;
            #endregion

            #region создать пользователя и закинуть его в бд
            var tempUser = new User(login, PasswordService.HashPassword(password), _roleService.GetRoleByName(role));
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
        public async Task<List<User>> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }
    }

}

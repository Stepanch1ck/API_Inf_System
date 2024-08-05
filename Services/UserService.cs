using RegisterOfInformationSystems.Core.Interfaces;
using RegisterOfInformationSystems.Core.Models;
using RegisterOfInformationSystems.Database;

namespace Services
{
    public class UserService(PostgresDbContext dbContext) : IActionsOnTheUser
    {
        private readonly PostgresDbContext _dbContext = dbContext;

        public async Task<bool> ChangePassword(string login, string newPassword)
        {
            var user = _dbContext.Users.Where(user => user.Login.ToLower() == login.ToLower()).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            _dbContext.Users.AddAsync(new User(login, newPassword));
            return true;
        }

        public async Task<bool> CreateUser(string login, string password)
        {
            if (_dbContext.Users.Where(user => user.Login.ToLower() == login.ToLower()).Any())
            {
                return false;
            }
            await _dbContext.Users.AddAsync(new User(login,password));
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(string login)
        {
            var user = _dbContext.Users.Where(user => user.Login.ToLower() == login.ToLower()).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetByLogin(string login)
        {
            var user = _dbContext.Users.Where(user => user.Login.ToLower() == login.ToLower()).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<List<User>?> GetByRole(string name)
        {
            var user = _dbContext.Users.Where(user => user.Role.Name.ToLower() == name.ToLower());
            if (user.FirstOrDefault() == null)
            {
                return null;
            }
            return user.ToList();
        }

        public async Task<bool> LogInPermission(string login, string password)
        {
            var user = _dbContext.Users.Where(user => user.Login.ToLower() == login.ToLower()).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            if(password.GetHashCode() == user.Password)
            {
                return true;
            }
            return false;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RegisterOfIS.Interfaces;
using RegisterOfIS.Models;
using RegisterOfIS.Database;

namespace RegisterOfIS.Services
{
    public class RoleService(PostgresDbContext dbContext) : IRoleService
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        public async Task<List<Role>> GetAllRoles()
        {
            #region Получить все роли
            return _dbContext.Roles.Include(r => r.Users).ToList();
            #endregion
        }

        public Role? GetRoleByName(string name)
        {
            #region найти и вернуть роль или null
            return _dbContext.Roles.Where(r => r.Name.Equals(name)).FirstOrDefault();
            #endregion
        }
    }
}

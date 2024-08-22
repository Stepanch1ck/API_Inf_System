using Microsoft.EntityFrameworkCore;
using RegistryOfInformationSystems.Interfaces;
using RegistryOfInformationSystems.Database;
using RegistryOfInformationSystems.Models;

namespace RegistryOfInformationSystems.Services
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

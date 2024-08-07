using Microsoft.EntityFrameworkCore;
using RegisterOfInformationSystems.Core.Interfaces;
using RegisterOfInformationSystems.Core.Models;
using RegisterOfInformationSystems.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Service
{
    public class RoleService(PostgresDbContext dbContext) : IActionsOnTheRoles
    {
        private readonly PostgresDbContext _dbContext = dbContext;
        public Role? GetRolePowerOf(int power)
        {
            #region Найти роль по доступу
            var role = _dbContext.Roles.Where(role => role.RolePower == power).FirstOrDefault();
            #endregion

            #region вернуть null если нет роли
            if (role == null) return null;
            #endregion

            #region вернуть роль
            return role;
            #endregion
        }
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

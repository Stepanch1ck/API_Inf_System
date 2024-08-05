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
        public async Task<Role> GetRolePowerOf(int power)
        {
            var role = _dbContext.Roles.Where(role => role.RolePower == power);
            if (role.FirstOrDefault() == null)
            {
                return null;
            }
            return role.FirstOrDefault();
        }
    }
}

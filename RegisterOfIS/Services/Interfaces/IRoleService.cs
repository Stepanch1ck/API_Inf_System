using RegisterOfIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Interfaces
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRoles();
        public Role? GetRoleByName(string name);
    }
}

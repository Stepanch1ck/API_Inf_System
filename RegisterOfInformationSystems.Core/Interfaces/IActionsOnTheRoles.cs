using RegisterOfInformationSystems.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Interfaces
{
    public interface IActionsOnTheRoles
    {
        public Role? GetRolePowerOf(int power);
        public Role? GetRoleByName(string name);
        public List<Role> GetAllRoles();
    }
}

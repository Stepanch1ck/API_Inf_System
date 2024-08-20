using RegisterOfIS.Models;

namespace RegisterOfIS.Interfaces
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRoles();
        public Role? GetRoleByName(string name);
    }
}

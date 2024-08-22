using RegistryOfInformationSystems.Models;

namespace RegistryOfInformationSystems.Interfaces
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRoles();
        public Role? GetRoleByName(string name);
    }
}

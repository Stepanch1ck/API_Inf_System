using Attribute = RegistryOfInformationSystems.Models.Attribute;

namespace RegistryOfInformationSystems.Interfaces
{
    public interface IAttributeService
    {
        Task<Attribute?> GetAttributeByFullName(string fullName);
        Task<List<Attribute>?> GetAttributesByPartOfName(string fullName);
        Task<List<Attribute>?> GetAttributesByStateContain(string state);
        Task<bool> CreateAttribute(string name, string description, List<string> states);
        Task<bool> DeleteAttribute(string name);
        Task<bool> AddNewState(string name, string state);
        Task<bool> RemoveState(string name, string state);
        Task<bool> ChangeAttribute(string name, string newName, string description);
    }
}

using RegistryOfInformationSystems.Models;

namespace RegistryOfInformationSystems.Interfaces
{
    public interface IInfSystemService
    {
        Task<bool> CreateInformationSystem(string name, string description, List<string> keys, List<string> values);
        Task<bool> DeleteInformationSystem(string name);
        Task<bool> UpdateInformationSystem(string name, string newName, string newDescription, List<string> keys, List<string> values);
        Task<InfSystem?> GetInformationSystemByFullName(string fullName);
        Task<List<InfSystem?>> GetInformationSystemsByPartOfName(string partOfName);

    }
}

using RegisterOfIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Interfaces
{
    public interface IInfSystemService
    {
        Task<bool> CreateInformationSystem(string name, string description, List<string> keys, List<string> values);
        Task<bool> DeleteInformationSystem(string name);
        Task<bool> UpdateInformationSystem(string name, List<string> keys, List<string> values);
        List<InfSystem> GetInformationSystemsByPartOfName(string partOfName);
        InfSystem? GetInformationSystemByFullName(string fullName);
        List<InfSystem> GetInformationSystemsByAttributeContain(string attribute);
        Task<bool> SetAttributeToInformationalSystem(string name, string attribute, string state);

    }
}

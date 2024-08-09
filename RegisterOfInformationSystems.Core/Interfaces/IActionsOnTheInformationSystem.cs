using RegisterOfInformationSystems.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Interfaces
{
    public interface IActionsOnTheInformationSystem
    {
        Task<bool> CreateInformationSystem(string name, string description, List<string> keys, List<string> values);
        Task<bool> DeleteInformationSystem(string name);
        Task<bool> UpdateInformationSystem(string name, List<string> keys, List<string> values);
        List<InformationSystem>? GetInformationSystemsByPartOfName(string partOfName);
        InformationSystem? GetInformationSystemByFullName(string fullName);
        List<InformationSystem>? GetInformationSystemsByAttributeContain(string attribute);
        Task<bool> SetAttributeToInformationalSystem(string name, string attribute, string state);

    }
}

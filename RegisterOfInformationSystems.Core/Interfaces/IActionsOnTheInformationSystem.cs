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
        Task<bool> CreateInformationSystem(string name, string description, Dictionary<Models.Attribute, string> attributes);
        Task<bool> DeleteInformationSystem(string name);
        Task<bool> UpdateInformationSystem(string name, Dictionary<Models.Attribute, string> attributes);
        Task<List<InformationSystem>> GetInformationSystemsByPartOfName(string partOfName);
        Task<List<InformationSystem>> GetInformationSystemsByAttributeContain(string state);
        Task<bool> SetAttributeToInformationalSystem(string name, Models.Attribute attribute, string state);

    }
}

using RegisterOfInformationSystems.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Interfaces
{
    public interface IActionsOnTheAttributes
    {
        Task<List<Models.Attribute>> GetAttributesByPartOfName(string partOfName);
        Task<List<Models.Attribute>> GetAttributesByStateContain(string state);
        Task<bool> CreateAttribute(string name, string description, List<string> states);
        Task<bool> DeleteAttribute(string name);
        Task<bool> AddNewState(string name, string state);
        Task<bool> RemoveState(string name, string state);
        Task<bool> SetAttributeToInformationalSystem(string name, ,string state);
    }
}

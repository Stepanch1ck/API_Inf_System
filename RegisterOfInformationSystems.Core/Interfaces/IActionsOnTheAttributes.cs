using RegisterOfInformationSystems.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = RegisterOfInformationSystems.Core.Models.Attribute;

namespace RegisterOfInformationSystems.Core.Interfaces
{
    public interface IActionsOnTheAttributes
    {
        List<Attribute>? GetAttributesByPartOfName(string partOfName);
        Attribute? GetAttributeByFullName(string fullName);
        List<Attribute>? GetAttributesByStateContain(string state);
        Task<bool> CreateAttribute(string name, string description, List<string> states);
        Task<bool> DeleteAttribute(string name);
        Task<bool> AddNewState(string name, string state);
        Task<bool> RemoveState(string name, string state);
    }
}

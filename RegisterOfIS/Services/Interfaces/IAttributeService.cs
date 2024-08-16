using RegisterOfIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = RegisterOfIS.Models.Attribute;

namespace RegisterOfIS.Interfaces
{
    public interface IAttributeService
    {
        List<Attribute>? GetAttributesByPartOfName(string partOfName);
        Task<Attribute?> GetAttributeByFullName(string fullName);
        List<Attribute>? GetAttributesByStateContain(string state);
        Task<bool> CreateAttribute(string name, string description, List<string> states);
        Task<bool> DeleteAttribute(string name);
        Task<bool> AddNewState(string name, string state);
        Task<bool> RemoveState(string name, string state);
        Task<bool> ChangeAttribute(string name, string newName, string description);
    }
}

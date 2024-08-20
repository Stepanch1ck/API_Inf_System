using RegisterOfIS.Requests;
using System.Diagnostics.CodeAnalysis;
using Attribute = RegisterOfIS.Models.Attribute;
using RegisterOfIS.Models;

namespace RegisterOfIS.Interfaces
{
    public interface IAttributeService
    {
        Attribute? Find([NotNull] int id);
        Attribute Get(int id);
        Task<List<Attribute>> Search(SearchAtrributesForm form);
        Task<Attribute?> Create(CreateAttributeRequest data);
        Task<Attribute?> Update(int id, ChangeAttributeRequest data);
        void Delete(int id);

        List<string> GetDict(int id);
        Task<bool> AddDict(int id, List<string> data);
        Task<bool> RemoveDict(int id, List<string> data);
    }
}

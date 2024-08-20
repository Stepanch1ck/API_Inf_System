using RegisterOfIS.Models;
using RegisterOfIS.Requests;
using System.Diagnostics.CodeAnalysis;

namespace RegisterOfIS.Interfaces
{
    public interface IInfSystemService
    {
        InfSystem? Find([NotNull] int id);
        InfSystem Get(int id);
        Task<List<InfSystem>> Search(SearchInfSystemsForm form);
        Task<InfSystem?> Create(CreateInfSystemRequest data);
        Task<InfSystem?> Update(int id, ChangeInfSystemRequest data);
        void Delete(int id);

        List<AttributeStateDto> GetAttrs(int id);
        Task<bool> AddAttrs(int id, Dictionary<int, string> data);
        Task<bool> RemoveAttrs(int id, List<int> data);

    }
}

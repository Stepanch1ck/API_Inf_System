using API_Inf_System.DBContext;
using API_Inf_System.Models.DTO;

namespace API_Inf_System.IServices
{
    public interface IAttributeValueService
    {
        Task<List<AttributeValueDto>> GetAllAttributeValuesAsync();
        Task<AttributeValue> GetAttributeValueByIdAsync(int id);
        Task<AttributeValue> CreateAttributeValueAsync(int informationSystemId, int attributeId, string value);
        Task<bool> UpdateAttributeValueAsync(int id, int informationSystemId, int attributeId, string value);
        Task<bool> DeleteAttributeValueAsync(int id);
    }
}

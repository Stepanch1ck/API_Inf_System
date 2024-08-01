using API_Inf_System.Models.DTO;
namespace API_Inf_System.IServices
{
    public interface IAttributeService
    {
        Task<List<AttributeDto>> GetAllAttributesAsync();
        Task<AttributeDto> GetAttributeByIdAsync(int id);
        Task<DBContext.Attribute> CreateAttributeAsync(AttributeCreateDto attributeDto);
        Task<bool> UpdateAttributeAsync(int id, AttributeDto attributeDto);
        Task<bool> DeleteAttributeAsync(int id);
        Task<AttributeDto> GetOrCreateAttributeByNameAsync(string attributeName, string dataType);
    }
}

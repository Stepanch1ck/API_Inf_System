using API_Inf_System.DBContext;
using API_Inf_System.DTO;
using API_Inf_System.Models.DTO;

namespace API_Inf_System.IServices
{
    public interface IInformationSystemService
    {
        Task<List<InformationSystemDto>> GetAllInformationSystemsAsync();
        Task<InformationSystemDto> GetInformationSystemByIdAsync(int id);
       // Task<InformationSystem> CreateInformationSystemAsync(InformationSystemCreateDto informationSystemDto);
        Task<bool> UpdateInformationSystemAsync(int id, InformationSystemUpdateDto informationSystemDto);
        Task<bool> UpdateInformationSystemAttributesAsync(int id, InformationSystemAttributeUpdateDto informationSystemDto);
        Task<bool> DeleteInformationSystemAsync(int id);
        Task<InformationSystemDetailsDto> GetInformationSystemDetailsAsync(int id);
        Task<bool> EditInformationSystemAsync(int id, InformationSystemEditDto informationSystemDto);
        Task<List<InformationSystemWithAttributesDto>> GetAllInformationSystemsWithAttributesAsync();
        Task<InformationSystemCreateDto> CreateInformationSystemWithAttributesAsync(InformationSystemCreateDto informationSystemDto);
        Task<bool> EditInformationSystemWithAttributesAsync(int id, InformationSystemEditDto informationSystemDto);
    }
}

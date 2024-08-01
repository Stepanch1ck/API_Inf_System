using System.ComponentModel.DataAnnotations;

namespace API_Inf_System.Models.DTO
{
    public class InformationSystemUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название информационной системы обязательно.")]
        public string Name { get; set; } = null!;

        public List<AttributeDto> Attributes { get; set; } = new List<AttributeDto>();
    }
}

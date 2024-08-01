using System.ComponentModel.DataAnnotations;

namespace API_Inf_System.Models.DTO
{
    public class InformationSystemCreateDto
    {
        [Required(ErrorMessage = "Название информационной системы обязательно.")]
        public string Name { get; set; } = null!;

        public List<AttributeWithValueDto> Attributes { get; set; } = new ();
    }
}

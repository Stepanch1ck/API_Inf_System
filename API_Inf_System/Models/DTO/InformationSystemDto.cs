using API_Inf_System.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace API_Inf_System.DTO
{
    public class InformationSystemDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название информационной системы обязательно.")]
        public string Name { get; set; } = null!;
        //public Dictionary<string, string> Attributes { get; set; } = new();
        public List<AttributeDto> Attributes { get; set; } = new List<AttributeDto>();
    }
}

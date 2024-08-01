using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Inf_System.Models.DTO
{
    public class AttributeCreateDto
    {
        [Required(ErrorMessage = "Название атрибута обязательно.")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Тип данных атрибута обязателен.")]
        [JsonPropertyName("dataType")]
        public string DataType { get; set; }
    }
}

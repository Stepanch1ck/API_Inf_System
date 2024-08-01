namespace API_Inf_System.Models.DTO
{
    public class InformationSystemEditDto
    {
        public string Name { get; set; }
        public List<AttributeWithValueDto> Attributes { get; set; } = new();
    }
}

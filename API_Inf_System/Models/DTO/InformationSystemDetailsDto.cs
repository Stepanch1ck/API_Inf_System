namespace API_Inf_System.Models.DTO
{
    public class InformationSystemDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AttributeWithValueDto> Attributes { get; set; } = new();
    }
}

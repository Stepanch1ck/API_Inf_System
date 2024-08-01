namespace API_Inf_System.Models.DTO
{
    public class AttributeValueDto
    {
        public int Id { get; set; }
        public int InformationSystemId { get; set; }
        public AttributeDto Attribute { get; set; } 
        public string Value { get; set; }
    }
}

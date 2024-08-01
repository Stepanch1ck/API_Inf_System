namespace API_Inf_System.Models.DTO
{
    public class InformationSystemAttributeUpdateDto
    {
        public Dictionary<string, List<string>> Attributes { get; set; } = new();
    }
}

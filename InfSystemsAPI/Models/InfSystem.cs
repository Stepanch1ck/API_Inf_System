namespace InfSystemsAPI.Models
{
    public class InfSystem(string name, string description, Dictionary<string, string> attributes)
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public Dictionary<string, string> Attributes { get; set;} = attributes;
    }
}

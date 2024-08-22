using Newtonsoft.Json;

namespace RegistryOfInformationSystems.Models
{
    public class InfSystem
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttributeStatement> AttributeStatements { get; set; } = [];
        public InfSystem() { }
        public InfSystem(string name, string description, List<AttributeStatement> statements)
        {
            Name = name;
            Description = description;
            AttributeStatements = statements;
        }
    }
}

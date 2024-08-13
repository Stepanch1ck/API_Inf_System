
using Newtonsoft.Json;

namespace RegisterOfIS.Models
{
    public class Attribute
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<AttributeStatement> States { get; set; } = [];
        public Attribute() { }
        public Attribute(string name, string description, List<AttributeStatement> states)
        {
            Name = name;
            Description = description;
            States = states;
        }
    }
}

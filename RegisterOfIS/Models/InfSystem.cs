
namespace RegisterOfIS.Models
{
    public class InfSystem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttributeState> AttributeStates { get; set; } = [];
        public ICollection<Attribute> Attributes { get; set; } = [];
        public InfSystem() { }
        public InfSystem(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}

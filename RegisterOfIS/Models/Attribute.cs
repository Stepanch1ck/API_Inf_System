
namespace RegisterOfIS.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFixed { get; set; }
        public ICollection<AttributeDict> Dict { get; set; } = [];
        public ICollection<AttributeState> States { get; set; } = [];
        public ICollection<InfSystem> InfSystems { get; set; } = [];
        public Attribute() { }
        public Attribute(string name, string description, bool isFixed)
        {
            Name = name;
            Description = description;
            IsFixed = isFixed;
        }

        public Attribute(string name, string description, bool isFixed, List<AttributeState> states)
        {
            Name = name;
            Description = description;
            IsFixed = isFixed;
            States = states;
        }
    }
}

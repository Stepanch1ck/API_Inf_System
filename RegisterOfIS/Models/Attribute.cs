
namespace RegisterOfIS.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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

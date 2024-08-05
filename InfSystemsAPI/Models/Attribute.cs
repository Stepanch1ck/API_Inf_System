namespace InfSystemsAPI.Models
{
    public class Attribute
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] States { get; set; }
        public Attribute() { }
        public Attribute(string name, string description, string[] states)
        {
            Name = name;
            Description = description;
            States = states;
        }
    }
}

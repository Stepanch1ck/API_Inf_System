namespace ReactApp2.Server.Models
{
    public class InfSystem(string name, string description)
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = name ;
        public string Description { get; set; } = description ;
    }
}

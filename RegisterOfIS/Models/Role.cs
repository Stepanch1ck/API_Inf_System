


namespace RegisterOfIS.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; } = [];
        public List<User> _users = new List<User>();

        public Role() { }
        public Role(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}



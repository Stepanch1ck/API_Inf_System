namespace InfSystemsAPI.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public Role(int power, string name, string description) 
        {
            this.RolePower = power;
            this.Name = name;
            this.Description = description;
        }
        public int RolePower { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Role() { }
    }
}

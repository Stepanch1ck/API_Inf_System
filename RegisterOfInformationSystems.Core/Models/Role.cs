using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public int RolePower { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; } = [];
        public List<User> _users = new List<User>();

        public Role() { }
        public Role(int power, string name, string description)
        {
            this.RolePower = power;
            this.Name = name;
            this.Description = description;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public int RolePower { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public ICollection<User> Users { get; set; } = [];
        public List<User> _users = [];
        public Role() { }
        public Role(int power, string name, string description)
        {
            this.RolePower = power;
            this.Name = name;
            this.Description = description;
        }
    }
}

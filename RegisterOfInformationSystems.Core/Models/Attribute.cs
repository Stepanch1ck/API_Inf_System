using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Models
{
    public class Attribute
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttributeStatement> States { get; set; } = [];
        public List<AttributeStatement> states { get; set; }
        public ICollection<InformationSystem> InformationSystems { get; set; }
        public Attribute() { }
        public Attribute(string name, string description, List<AttributeStatement> states)
        {
            Name = name;
            Description = description;
            States = states;
        }
    }
}

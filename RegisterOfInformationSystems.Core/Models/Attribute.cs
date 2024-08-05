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
        public List<string> States { get; set; }
        public Attribute(string name, string description, List<string> states)
        {
            Name = name;
            Description = description;
            States = states;
        }
    }
}

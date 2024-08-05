using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Models
{
    public class InformationSystem(string name, string description, Dictionary<Attribute, string> attributes)
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public Dictionary<Attribute, string> Attributes { get; set; } = attributes;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Models
{
    public class InformationSystem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttributeStatement> AttributeStatements { get; set; } = [];
        public ICollection<Attribute> Attributes { get; set; } = [];
        public InformationSystem() { }
        public InformationSystem(string name, string description, List<Attribute> attributes, List<AttributeStatement> statements)
        {
            Name = name;
            Description = description;
            Attributes = attributes;
            AttributeStatements = statements;
        }
    }
}

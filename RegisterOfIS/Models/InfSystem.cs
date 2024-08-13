using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Models
{
    public class InfSystem
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttributeStatement> AttributeStatements { get; set; } = [];
        [JsonIgnore]
        public ICollection<Attribute> Attributes { get; set; } = [];
        public InfSystem() { }
        public InfSystem(string name, string description, List<AttributeStatement> statements)
        {
            Name = name;
            Description = description;
            AttributeStatements = statements;
        }
    }
}

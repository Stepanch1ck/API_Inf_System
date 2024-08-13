using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RegisterOfIS.Models
{
    public class AttributeStatement
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public ICollection<InfSystem> InfSystems { get; set; } = [];
        public AttributeStatement()
        {
            
        }
        public AttributeStatement(Attribute attribute, string value)
        {
            this.Attribute = attribute;
            this.Value = value;
        }
    }
}

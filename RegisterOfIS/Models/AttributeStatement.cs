using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Models
{
    public class AttributeStatement
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public string Value { get; set; }
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

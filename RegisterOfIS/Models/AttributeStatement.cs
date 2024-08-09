using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfIS.Models
{
    public class AttributeStatement
    {
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public string Value { get; set; }
        public Guid InfSystemId { get; set; }
        public InfSystem InfSystem { get; set; }
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

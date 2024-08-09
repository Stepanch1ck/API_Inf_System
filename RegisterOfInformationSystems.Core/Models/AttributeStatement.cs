using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterOfInformationSystems.Core.Models
{
    public class AttributeStatement
    {
        public int Id { get; set; }
        public Attribute attribute { get; set; }
        public string Value { get; set; }
        public int AttributeId { get; set; }
        public ICollection<InformationSystem> InformationSystems { get; set; }= new List<InformationSystem>();
        public AttributeStatement()
        {
            
        }
        public AttributeStatement(Attribute attribute, string value)
        {
            this.attribute = attribute;
            this.Value = value;
        }
    }
}

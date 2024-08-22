using System.Text.Json.Serialization;

namespace RegistryOfInformationSystems.Models
{
    public class AttributeStatement
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int AttributeId { get; set; }
        [JsonIgnore]
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

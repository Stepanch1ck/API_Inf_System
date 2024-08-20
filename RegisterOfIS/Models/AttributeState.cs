
namespace RegisterOfIS.Models
{
    public class AttributeState
    {
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public int InfSystemId { get; set; }
        public InfSystem InfSystem { get; set; }
        public string Value { get; set; }
        public AttributeState()
        {
            
        }
        public AttributeState(int attributeId, int infSystemId, string value)
        {
            AttributeId = attributeId;
            InfSystemId = infSystemId;
            Value = value;
        }

        public AttributeState(Attribute attribute, InfSystem infSystem, string value)
        {
            this.Attribute = attribute;
            this.InfSystem = infSystem;
            this.Value = value;
        }
    }
}

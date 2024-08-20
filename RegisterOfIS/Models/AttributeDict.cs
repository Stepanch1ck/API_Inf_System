namespace RegisterOfIS.Models
{
    public class AttributeDict
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public string Value { get; set; }

        public AttributeDict() { }

        public AttributeDict(Attribute attribute, string value)
        {
            this.Attribute = attribute;
            this.Value = value;
        }
    }
}

using System;
using System.Collections.Generic;

namespace API_Inf_System.DBContext;

public partial class AttributeValue
{
    public int Id { get; set; }

    public int InformationSystemId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Attribute? Attribute { get; set; }

    public virtual InformationSystem? InformationSystem { get; set; }
}

using System;
using System.Collections.Generic;

namespace API_Inf_System.DBContext;

public partial class Attribute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DataType { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
}

using System;
using System.Collections.Generic;

namespace API_Inf_System.DBContext;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Role? Role { get; set; }
}

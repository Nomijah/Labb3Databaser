using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepName { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; } = new List<Staff>();
}

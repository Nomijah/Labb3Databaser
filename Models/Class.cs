using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public int? TeacherId { get; set; }

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual Teacher? Teacher { get; set; }
}

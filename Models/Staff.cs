using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? SocialNr { get; set; }

    public string? FName { get; set; }

    public string? LName { get; set; }

    public string? Position { get; set; }

    public int? AddressId { get; set; }

    public virtual AddressBook? Address { get; set; }

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}

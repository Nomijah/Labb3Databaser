using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class AddressBook
{
    public int AddressId { get; set; }

    public string? Street { get; set; }

    public int? PostalCode { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Staff> Staff { get; } = new List<Staff>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}

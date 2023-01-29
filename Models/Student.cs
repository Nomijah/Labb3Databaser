using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? SocialNr { get; set; }

    public string? FName { get; set; }

    public string? LName { get; set; }

    public int? AddressId { get; set; }

    public int? ClassId { get; set; }

    public virtual AddressBook? Address { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Grade> Grades { get; } = new List<Grade>();
}

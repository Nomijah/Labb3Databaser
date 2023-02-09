using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class VwAvgSalaryDep
{
    public string Avdelning { get; set; } = null!;

    public string? Medellön { get; set; }
}

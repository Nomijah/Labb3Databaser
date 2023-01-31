using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class VwGradesLastMonth
{
    public string? Namn { get; set; }

    public string? Kurs { get; set; }

    public string? Betyg { get; set; }

    public DateTime? Datum { get; set; }
}

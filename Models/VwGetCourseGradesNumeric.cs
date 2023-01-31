using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class VwGetCourseGradesNumeric
{
    public string? Kurs { get; set; }

    public int? Medelbetyg { get; set; }

    public int? HögstaBetyg { get; set; }

    public int? LägstaBetyg { get; set; }
}

using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class VwGetCourseGradesAlpha
{
    public string? Kurs { get; set; }

    public string? Medelbetyg { get; set; }

    public string? HögstaBetyg { get; set; }

    public string? LägstaBetyg { get; set; }
}

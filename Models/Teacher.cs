using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public int? StaffId { get; set; }

    public virtual ICollection<Class> Classes { get; } = new List<Class>();

    public virtual ICollection<Grade> Grades { get; } = new List<Grade>();

    public virtual Staff? Staff { get; set; }

    public virtual ICollection<TeacherCourse> TeacherCourses { get; } = new List<TeacherCourse>();
}

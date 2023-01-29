using System;
using System.Collections.Generic;

namespace Labb3Databaser.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public virtual ICollection<Grade> Grades { get; } = new List<Grade>();

    public virtual ICollection<TeacherCourse> TeacherCourses { get; } = new List<TeacherCourse>();
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Labb3Databaser.Models;

public partial class Labb2_SkolanDbContext : DbContext
{
    public Labb2_SkolanDbContext()
    {
    }

    public Labb2_SkolanDbContext(DbContextOptions<Labb2_SkolanDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressBook> AddressBooks { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherCourse> TeacherCourses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = LAPTOP-PCBVJLD6;Initial Catalog = Labb2_Skolan;Integrated Security = True;TrustServerCertificate = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressBook>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__AddressB__091C2A1BF4C9D367");

            entity.ToTable("AddressBook");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A002961FE3");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(10);
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Classes__Teacher__2E1BDC42");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D7187D138FB8D");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(50);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A374F365CB7");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Grade1)
                .HasMaxLength(3)
                .HasColumnName("Grade");
            entity.Property(e => e.GradeDate).HasColumnType("date");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Grades_Courses");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Grades_Students");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Grades__TeacherI__34C8D9D1");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7CC91BA98");

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .HasColumnName("fName");
            entity.Property(e => e.LName)
                .HasMaxLength(50)
                .HasColumnName("lName");
            entity.Property(e => e.Position).HasMaxLength(20);
            entity.Property(e => e.SocialNr).HasMaxLength(13);

            entity.HasOne(d => d.Address).WithMany(p => p.Staff)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Staff__AddressID__267ABA7A");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A79F564061C");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.FName)
                .HasMaxLength(50)
                .HasColumnName("fName");
            entity.Property(e => e.LName)
                .HasMaxLength(50)
                .HasColumnName("lName");
            entity.Property(e => e.SocialNr).HasMaxLength(13);

            entity.HasOne(d => d.Address).WithMany(p => p.Students)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Students__Addres__30F848ED");

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Students__ClassI__31EC6D26");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teachers__EDF259446BBF866A");

            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");

            entity.HasOne(d => d.Staff).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Teachers__StaffI__29572725");
        });

        modelBuilder.Entity<TeacherCourse>(entity =>
        {
            entity.HasKey(e => e.Tcid).HasName("PK__TeacherC__B773707FFE3DD036");

            entity.ToTable("TeacherCourse");

            entity.Property(e => e.Tcid).HasColumnName("TCID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

            entity.HasOne(d => d.Course).WithMany(p => p.TeacherCourses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__TeacherCo__Cours__3D5E1FD2");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherCourses)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__TeacherCo__Teach__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using CampusOrientationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusOrientationAPI.Data;

public partial class CampusOrientationDbContext : DbContext
{
    public CampusOrientationDbContext()
    {
    }

    public CampusOrientationDbContext(DbContextOptions<CampusOrientationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Backupclass> Backupclasses { get; set; }

    public virtual DbSet<Backupperson> Backuppeople { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Completeclass> Completeclasses { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=campusorientation;Username=apiacess;Password=apiacessuse");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Backupclass>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("backupclass");

            entity.HasIndex(e => e.Datetime, "indexnamebackupclass");

            entity.Property(e => e.Backuptime).HasColumnName("backuptime");
            entity.Property(e => e.Classroom)
                .HasMaxLength(11)
                .HasColumnName("classroom");
            entity.Property(e => e.Currentuser)
                .HasMaxLength(255)
                .HasColumnName("currentuser");
            entity.Property(e => e.Datetime).HasColumnName("datetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Idcourse)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("idcourse");
        });

        modelBuilder.Entity<Backupperson>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("backupperson");

            entity.HasIndex(e => e.Name, "indexnamebackupperson");

            entity.Property(e => e.Backuptime).HasColumnName("backuptime");
            entity.Property(e => e.Currentuser)
                .HasMaxLength(255)
                .HasColumnName("currentuser");
            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Ra)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("ra");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => new { e.Idcourse, e.Datetime }).HasName("class_pk");

            entity.ToTable("class");

            entity.HasIndex(e => e.Datetime, "indexclass");

            entity.Property(e => e.Idcourse)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("idcourse");
            entity.Property(e => e.Datetime).HasColumnName("datetime");
            entity.Property(e => e.Classroom)
                .HasMaxLength(11)
                .HasColumnName("classroom");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.IdcourseNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.Idcourse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("class_fk");
        });

        modelBuilder.Entity<Completeclass>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("completeclass");

            entity.Property(e => e.Classroom)
                .HasMaxLength(11)
                .HasColumnName("classroom");
            entity.Property(e => e.Coursename)
                .HasMaxLength(255)
                .HasColumnName("coursename");
            entity.Property(e => e.Datetime).HasColumnName("datetime");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Nameteacher)
                .HasMaxLength(255)
                .HasColumnName("nameteacher");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("course_pk");

            entity.ToTable("course");

            entity.HasIndex(e => e.Name, "indexcourse");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasDefaultValueSql("gen_random_uuid()")
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Idteacher)
                .HasMaxLength(36)
                .IsFixedLength()
                .HasColumnName("idteacher");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.IdteacherNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Idteacher)
                .HasConstraintName("teaches_course_fk");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pk");

            entity.ToTable("person");

            entity.HasIndex(e => e.Name, "indexnameperson");

            entity.HasIndex(e => e.Ra, "person_ra_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasDefaultValueSql("gen_random_uuid()")
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Ra)
                .HasMaxLength(8)
                .HasDefaultValueSql("generate_ra()")
                .IsFixedLength()
                .HasColumnName("ra");

            entity.HasMany(d => d.Idcourses).WithMany(p => p.Idstudents)
                .UsingEntity<Dictionary<string, object>>(
                    "Study",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("Idcourse")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("study_course_fk"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("Idstudent")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("study_student_fk"),
                    j =>
                    {
                        j.HasKey("Idstudent", "Idcourse").HasName("study_pk");
                        j.ToTable("study");
                        j.IndexerProperty<string>("Idstudent")
                            .HasMaxLength(36)
                            .IsFixedLength()
                            .HasColumnName("idstudent");
                        j.IndexerProperty<string>("Idcourse")
                            .HasMaxLength(36)
                            .IsFixedLength()
                            .HasColumnName("idcourse");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

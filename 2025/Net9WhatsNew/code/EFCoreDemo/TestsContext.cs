using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo;

public partial class TestsContext : DbContext
{
    public TestsContext()
    {
    }

    public TestsContext(DbContextOptions<TestsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

//andrei
partial void AfterOnConfiguring(DbContextOptionsBuilder optionsBuilder);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=tests;UID=sa;PWD=yourStrong(!)Password;TrustServerCertificate=true");
        AfterOnConfiguring(optionsBuilder);
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Iddepartment);

            entity.ToTable("Department");

            entity.Property(e => e.Iddepartment).HasColumnName("IDDepartment");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Idemployee);

            entity.ToTable("Employee");

            entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");
            entity.Property(e => e.Iddepartment).HasColumnName("IDDepartment");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IddepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Iddepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("test");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

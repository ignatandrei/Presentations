using System;
using System.Collections.Generic;
using DBData.genDBModels;
using Microsoft.EntityFrameworkCore;

namespace DBData.genContext;
//dotnet new install Microsoft.EntityFrameworkCore.Templates
//dotnet new ef-templates
//dotnet tool update --global dotnet-ef
//dotnet ef dbcontext scaffold "Data Source=127.0.0.1;Initial Catalog=XpertContract;UID=sa;PWD=P@ssw0rd;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer  --context-dir genContext --output-dir genDBModels  --force --no-pluralize --no-onconfiguring
public partial class EmpDepContext : DbContext
{
    public EmpDepContext(DbContextOptions<EmpDepContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Department { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07FE69EB1A");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0790B214EB");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Employee)
                .HasForeignKey(d => d.IdDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

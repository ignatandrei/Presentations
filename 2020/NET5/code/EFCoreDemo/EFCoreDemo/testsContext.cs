using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EFCoreDemo
{
    public partial class testsContext : DbContext
    {
        public testsContext()
        {
        }

        public testsContext(DbContextOptions<testsContext> options)
            : base(options)
        {
        }

        public DbSet<Dictionary<string, object>> DepartmentWithNumber => Set<Dictionary<string, object>>("DepNr");

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=tests;uid=sa;pwd=yourStrong(!)Password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("DepNr", b =>
            {
                b.HasNoKey();
                b.IndexerProperty<string>("Name");
                b.IndexerProperty<int>("Nr");
                b.IndexerProperty<string>("IDDepartment").IsRequired();
                b.ToSqlQuery(@" select d.IDDepartment, Count(*) as nr 
                    from Department d left join Employee e
                    on d.IDDepartment= e.IDDepartment
                    group by d.IDDepartment"
                    );
            });


            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Iddepartment);

                entity.ToTable("Department");

                entity.Property(e => e.Iddepartment).HasColumnName("IDDepartment");

                entity.Property(e => e.Name)
                    .IsRequired()
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
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IddepartmentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Iddepartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("test");

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
}

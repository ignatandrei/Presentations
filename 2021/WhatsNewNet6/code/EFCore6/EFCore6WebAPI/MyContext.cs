#nullable disable

namespace EFCore6WebAPI;

public partial class MyContext : DbContext
{
    public MyContext()
    {
    }

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(1024);
        configurationBuilder
            .IgnoreAny<TenantProp>();

        configurationBuilder
            .Properties<bool>()
            .HaveConversion<BoolToZeroOneConverter<int>>();

        base.ConfigureConventions(configurationBuilder);
    }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //modelBuilder.

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Iddepartment);

            entity.ToTable("Department");

            entity.Property(e => e.Iddepartment)
                .HasColumnName("IDDepartment");

            entity.Property(e => e.Name)
                .IsRequired()
                //.HasMaxLength(50)
                //.IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Idemployee);

            entity.ToTable("Employee");

            entity.Property(e => e.Idemployee).HasColumnName("IDEmployee");

            entity.Property(e => e.Iddepartment).HasColumnName("IDDepartment");

            entity.Property(e => e.Name)
                .IsRequired()
                //.HasMaxLength(100)
                //.IsUnicode(false);

            entity.HasOne(d => d.IddepartmentNavigation)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.Iddepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Department");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


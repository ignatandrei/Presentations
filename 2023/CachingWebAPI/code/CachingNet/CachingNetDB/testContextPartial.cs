namespace CachingNetDB;

partial class TestContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>().ToTable(tb => tb.HasTrigger("tr_dbo_Department_d00a499e-1f82-4182-ad4a-34fb943228c0_Sender"));

    }
}

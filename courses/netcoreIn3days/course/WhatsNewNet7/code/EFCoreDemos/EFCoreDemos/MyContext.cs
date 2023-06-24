namespace EFCoreDemos;

public partial class TestsContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SetRetrievedInterceptor());
        base.OnConfiguring(optionsBuilder);
    }
}

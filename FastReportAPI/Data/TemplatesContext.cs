namespace FastReportAPI.Data;
public class TemplatesContext : DbContext
{
    public DbSet<Template> Templates { get; set; }
    public TemplatesContext(DbContextOptions<TemplatesContext> options) : base(options)
    { }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Template>().HasData(
            new Template
            {
                Id = 1,
                Name = "Акт возобновления качественного предоставления услуг",
            }
            );
    }
}
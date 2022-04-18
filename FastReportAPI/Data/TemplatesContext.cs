namespace FastReportAPI.Data;
public class TemplatesContext : DbContext
{
    public DbSet<Template> Templates { get; set; }
    public TemplatesContext(DbContextOptions<TemplatesContext> options) : base(options)
        => Database.Migrate();

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Template>().HasData(
            new Template
            {
                Id = 1,
                Name = "Тестовый шаблон",
                Path = "\\Акт возобновления качественного предоставления услуг"
            }
            );
    }
}
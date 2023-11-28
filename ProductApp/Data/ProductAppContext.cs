using ProductApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ProductApp.Data;

public class ProductAppContext : DbContext
{
    public ProductAppContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>()
            .HavePrecision(2);
    }
}

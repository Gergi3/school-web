using ProductApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ProductApp.Data;

public class ProductAppContext : DbContext
{
    public ProductAppContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;
}

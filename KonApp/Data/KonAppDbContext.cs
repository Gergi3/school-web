using KonApp.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KonApp.Data;

public class KonAppDbContext : IdentityDbContext
{
	public KonAppDbContext(DbContextOptions<KonAppDbContext> options)
		: base(options)
	{
	}

	public DbSet<Breed> Breeds { get; set; }
	public DbSet<Horse> Horses { get; set; }
}

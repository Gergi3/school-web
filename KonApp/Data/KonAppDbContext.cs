using KonApp.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KonApp.Data;

public class KonAppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
	public KonAppDbContext(DbContextOptions<KonAppDbContext> options)
		: base(options) { }

	public DbSet<Breed> Breeds { get; set; } = default!;
	public DbSet<Horse> Horses { get; set; } = default!;
}

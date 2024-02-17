using BooksApp.Infrastructure.Data.Configuration;
using BooksApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Infrastructure.Data;

/// <summary>
/// Represents the application's database context, extending IdentityDbContext with customized user, role, and identifier types.
/// This class manages the interactions between the application and the database, including user and role management.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
	/// <summary>
	/// Initializes a new instance of the ApplicationDbContext class with the specified options for database context configuration.
	/// </summary>
	/// <param name="options">The options to be used for configuring the database context.</param>
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Book> Books { get; set; }
	public DbSet<UserBook> UserBook { get; set; }
	public DbSet<Publisher> Publishers { get; set; }

	/// <summary>
	/// Configures the model that is used to define the database schema.
	/// </summary>
	/// <param name="builder">The model builder to be used for configuration.</param>
	protected override void OnModelCreating(ModelBuilder builder)
	{
		//builder
		//	.Entity<UserBook>()
		//	.HasKey(vf => new { vf.UserId, vf.BookId });

		builder
			.Entity<User>()
			.HasMany(u => u.Books)
			.WithMany(b => b.Users)
			.UsingEntity<UserBook>(
				ub => ub.HasKey(x => new { x.UserId, x.BookId })
			);

		builder.ApplyConfiguration(new UserConfiguration());
		builder.ApplyConfiguration(new RoleConfiguration());
		builder.ApplyConfiguration(new UserRoleConfiguration());
		builder.ApplyConfiguration(new PublisherConfiguration());
		builder.ApplyConfiguration(new BookConfiguration());

		base.OnModelCreating(builder);
	}
}

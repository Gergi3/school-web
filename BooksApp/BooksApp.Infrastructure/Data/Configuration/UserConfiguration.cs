using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BooksApp.Infrastructure.Data.Models;

namespace BooksApp.Infrastructure.Data.Configuration;

/// <summary>
/// Contains methods for configuring User to have default values
/// </summary>
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	/// <summary>
	/// Adds default users to builder
	/// </summary>
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasData(CreateUsers());
	}

	/// <summary>
	/// Creates the default users
	/// </summary>
	private static List<User> CreateUsers()
	{
		var users = new List<User>();
		var hasher = new PasswordHasher<User>();

		string email = "user@mail.com";
		var user = new User()
		{
			Id = new Guid(ConfigurationConstants.UserId),
			UserName = email,
			NormalizedUserName = email,
			Email = email,
			NormalizedEmail = email,
		};

		user.PasswordHash = hasher.HashPassword(user, "user123");
		user.SecurityStamp = Guid.NewGuid().ToString();
		users.Add(user);

		email = "admin@mail.com";
		user = new User()
		{
			Id = new Guid(ConfigurationConstants.AdminId),
			UserName = email,
			NormalizedUserName = email,
			Email = email,
			NormalizedEmail = email
		};

		user.PasswordHash = hasher.HashPassword(user, "admin123");
		user.SecurityStamp = Guid.NewGuid().ToString();
		users.Add(user);

		return users;
	}
}

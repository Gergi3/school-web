using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BooksApp.Infrastructure.Data.Configuration;

/// <summary>
/// Contains methods for configuring IdentityRole to have default values
/// </summary>
internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
	/// <summary>
	/// Adds default roles to builder
	/// </summary>
	public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
	{
		builder.HasData(CreateRoles());
	}

	/// <summary>
	/// Creates the default roles
	/// </summary>
	private static List<IdentityRole<Guid>> CreateRoles()
	{
		var roles = new List<IdentityRole<Guid>>();

		var role = new IdentityRole<Guid>()
		{
			Id = new Guid(ConfigurationConstants.AdminRoleId),
			Name = "Admin"
		};
		roles.Add(role);

		role = new IdentityRole<Guid>()
		{
			Id = new Guid(ConfigurationConstants.UserRoleId),
			Name = "User"
		};
		roles.Add(role);

		return roles;
	}
}

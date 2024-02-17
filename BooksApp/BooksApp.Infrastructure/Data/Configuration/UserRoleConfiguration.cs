using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BooksApp.Infrastructure.Data.Configuration;

/// <summary>
/// Contains methods for configuring IdentityUserRole to have default values
/// </summary>
internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
	/// <summary>
	/// Adds default user roles to builder
	/// </summary>
	public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
	{
		builder.HasData(CreateUserRoles());
	}

	/// <summary>
	/// Creates the default user roles
	/// </summary>
	private static List<IdentityUserRole<Guid>> CreateUserRoles()
	{
		var userRoles = new List<IdentityUserRole<Guid>>();

		var userRole = new IdentityUserRole<Guid>()
		{
			RoleId = new Guid(ConfigurationConstants.AdminRoleId),
			UserId = new Guid(ConfigurationConstants.AdminId),
		};
		userRoles.Add(userRole);

		userRole = new IdentityUserRole<Guid>()
		{
			RoleId = new Guid(ConfigurationConstants.UserRoleId),
			UserId = new Guid(ConfigurationConstants.UserId)
		};
		userRoles.Add(userRole);

		return userRoles;
	}
}

using Microsoft.AspNetCore.Identity;

namespace KonApp.Models.Domain;

public class User : IdentityUser<Guid>
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
}

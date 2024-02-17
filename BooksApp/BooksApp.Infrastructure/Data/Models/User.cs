using Microsoft.AspNetCore.Identity;

namespace BooksApp.Infrastructure.Data.Models;

/// <summary>
/// Represents a user entity inheriting from IdentityUser with a Guid as the identifier.
/// </summary>
public class User : IdentityUser<Guid>
{
	public ICollection<UserBook> UserBooks { get; } = [];
	public ICollection<Book> Books { get; } = [];
}

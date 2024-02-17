using System.ComponentModel.DataAnnotations;

namespace BooksApp.Core.Models;

/// <summary>
/// View model transfering user data when logging in
/// </summary>
public class LoginViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = default!;

	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; } = default!;
}

using System.ComponentModel.DataAnnotations;

namespace BooksApp.Core.Models;

/// <summary>
/// View model transfering user data when registering in
/// </summary>
public class RegisterViewModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = default!;

	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; } = default!;

	[Required]
	[DataType(DataType.Password)]
	[Compare("Password")]
	public string RepeatPassword { get; set; } = default!;

}

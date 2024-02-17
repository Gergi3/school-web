using System.ComponentModel.DataAnnotations;

namespace BooksApp.Core.Models;

/// <summary>
/// View model transfering Publisher data
/// </summary>
public class PublisherViewModel
{
	[Required]
	public Guid Id { get; set; }

	[Required]
	[StringLength(150, MinimumLength = 1)]
	public string Name { get; set; } = null!;
}

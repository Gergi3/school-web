using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.Core.Models;

/// <summary>
/// View model transfering Book data
/// </summary>
public class BookViewModel
{
	[Required]
	public Guid Id { get; set; }

	[Required]
	[StringLength(100, MinimumLength = 2)]
	public string Title { get; set; } = null!;

	[Required]
	[Range(0, 2024)]
	public int Year { get; set; }

	[Required]
	[StringLength(35, MinimumLength = 5)]
	public string Author { get; set; } = null!;

	[Required]
	public string ISBN { get; set; } = null!;

	[Required]
	[RegularExpression(
		"(https?://.*.(?:png|jpg|jpeg|gif|png|svg|webp))",
		ErrorMessage = "The Image URL needs to be a valid url"
	)]
	[DisplayName("Image URL")]
	public string ImageUrl { get; set; } = null!;

	public string? PublisherName { get; set; }
	public Guid PublisherId { get; set; }
	public Guid[] ReadBy { get; set; } = [];
	public bool IsRead { get; set; }
}

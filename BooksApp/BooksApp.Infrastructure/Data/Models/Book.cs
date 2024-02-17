using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Infrastructure.Data.Models;

/// <summary>
/// Book entity used to represent a real life book.
/// </summary>
public class Book
{
	[Key]
	[Comment("Unique identificator of the book")]
	public Guid Id { get; set; }

	[Required]
	[StringLength(100, MinimumLength = 2)]
	[Comment("Title of the book")]
	public string Title { get; set; } = null!;

	[Required]
	[Comment("Year in which the book was published")]
	public int Year { get; set; }

	[Required]
	[StringLength(35, MinimumLength = 5)]
	[Comment("Author by which the book was published")]
	public string Author { get; set; } = null!;

	[Required]
	[Comment("String representation of the barcode of the book")]
	public string ISBN { get; set; } = null!;

	[Required]
	[Comment("Image URL of the cover of the book")]
	public string ImageUrl { get; set; } = null!;

	[ForeignKey(nameof(Publisher))]
	[Comment("Published ID by which the book was published")]
	public Guid PublisherId { get; set; }
	public Publisher Publisher { get; set; } = null!;

	public ICollection<UserBook> UserBooks { get; } = [];
	public ICollection<User> Users { get; } = [];
}

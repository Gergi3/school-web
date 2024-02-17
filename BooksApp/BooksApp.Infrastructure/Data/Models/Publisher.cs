using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Infrastructure.Data.Models;

/// <summary>
/// Publisher entity used to represent a book's publisher.
/// </summary>
public class Publisher
{
	[Key]
	[Comment("Unique identificator of the publisher")]
	public Guid Id { get; set; }

	[Required]
	[StringLength(150, MinimumLength = 1)]
	[Comment("Name of the publisher")]
	public string Name { get; set; } = null!;

	public ICollection<Book> Books { get; set; } = null!;
}

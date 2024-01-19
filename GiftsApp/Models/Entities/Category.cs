using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftsApp.Models.Entities;

public class Category
{
	[Key]
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;

	[Column(TypeName = "datetime")]
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}

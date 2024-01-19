using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftsApp.Models.Entities;

public class Gift
{
	[Key]
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public int MinAge { get; set; }
	public int MaxAge { get; set; }
	[ForeignKey(nameof(Entities.Category))]
	public Guid CategoryId { get; set; }
	public Category Category { get; set; } = default!;

	[Column(TypeName = "datetime")]
	public DateTime CreatedAt { get; set; } = DateTime.Now;
}

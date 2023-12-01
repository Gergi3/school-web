using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KonApp.Models.Domain;

public class Horse
{
	[Key]
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public int Age { get; set; }
	public string ImageUrl { get; set; } = null!;

	[ForeignKey(nameof(Domain.Breed))]
	public Guid BreedId { get; set; }
	public Breed Breed { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KonApp.Models.Domain;

public class Breed
{
	[Key]
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;

	[InverseProperty("Breed")]
	public ICollection<Horse> Horses { get; set; } = null!;
}

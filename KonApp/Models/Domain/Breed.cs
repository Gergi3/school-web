using System.ComponentModel.DataAnnotations;

namespace KonApp.Models.Domain;

public class Breed
{
	[Key]
	public int Id { get; set; }
	[Required, MinLength(1), MaxLength(100)]
	public string Name { get; set; } = null!;
}

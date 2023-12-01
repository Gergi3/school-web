using System.ComponentModel.DataAnnotations;

namespace KonApp.ViewModels;

public class BreedViewModel
{
	public Guid Id { get; set; }

	[Required, MinLength(1), MaxLength(100)]
	public string Name { get; set; } = null!;
}

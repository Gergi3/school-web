using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using KonApp.Constants;

namespace KonApp.Models.Domain;

public class Horse
{
	[Key]
	public int Id { get; set; }

	[Required, MinLength(2), MaxLength(10)]
	public string Name { get; set; } = null!;

	[Required, Range(0, 60)]
	public int Age { get; set; }

	[Required, RegularExpression(Regexes.URL_REGEX)]
	public string ImageUrl { get; set; } = null!;

	[ForeignKey(nameof(Domain.Breed))]
	public int BreedId { get; set; }
	public Breed Breed { get; set; } = null!;
}

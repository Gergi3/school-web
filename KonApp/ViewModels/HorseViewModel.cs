using KonApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace KonApp.ViewModels;

public class HorseViewModel
{
	public Guid Id { get; set; }

	[Required, MinLength(2), MaxLength(10)]
	public string Name { get; set; } = null!;

	[Required, Range(0, 60)]
	public int Age { get; set; }

	[Required, RegularExpression(Regexes.URL_REGEX)]
	public string ImageUrl { get; set; } = null!;
}

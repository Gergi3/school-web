using System.ComponentModel;

namespace GiftsApp.ViewModels;

public class CategoryViewModel
{
	public Guid Id { get; set; }

	[DisplayName("Наименование")]
	public required string Name { get; set; }
}

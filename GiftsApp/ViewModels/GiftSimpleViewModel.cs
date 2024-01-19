using System.ComponentModel;

namespace GiftsApp.ViewModels;

public class GiftSimpleViewModel
{
	public Guid Id { get; set; }

	[DisplayName("Наименование")]
	public required string Name { get; set; }

	[DisplayName("Начална възраст")]
	public required int MinAge { get; set; }

	[DisplayName("Крайна възраст")]
	public required int MaxAge { get; set; }

	[DisplayName("Категория ID")]
	public Guid CategoryId { get; set; }

	[DisplayName("Дата на добавяне")]
	public DateTime CreatedAt { get; set; }
}

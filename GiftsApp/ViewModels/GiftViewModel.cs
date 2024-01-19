using System.ComponentModel;

namespace GiftsApp.ViewModels;

public class GiftViewModel
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

	[DisplayName("Категория")]
	public string CategoryName { get; set; } = default!;

	[DisplayName("Дата на добавяне")]
	public DateTime CreatedAt { get; set; }
}

namespace BooksApp.Core.Models;

/// <summary>
/// View model transfering Errors data
/// </summary>
public class ErrorViewModel
{
	public string? RequestId { get; set; }

	public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
}

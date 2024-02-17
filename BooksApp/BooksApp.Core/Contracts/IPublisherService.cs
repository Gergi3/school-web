using BooksApp.Core.Models;

namespace BooksApp.Core.Contracts;

/// <summary>
/// Publisher service responsible for managing all the publishers business logic.
/// </summary>
public interface IPublisherService
{
	/// <summary>
	/// Fetches all publishers from the database
	/// </summary>
	/// <returns>All publishers as publisher view models</returns>
	Task<IEnumerable<PublisherViewModel>> All();

	/// <summary>
	/// Adds a publisher to the database
	/// </summary>
	/// <param name="viewModel">Publisher data</param>
	Task Add(PublisherViewModel viewModel);
}

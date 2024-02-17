using BooksApp.Core.Models;
using BooksApp.Infrastructure.Data.Models;

namespace BooksApp.Core.Contracts;

/// <summary>
/// Book service responsible for managing all the books business logic.
/// </summary>
public interface IBookService
{
	/// <summary>
	/// Fetches all books from the database
	/// </summary>
	/// <returns>All books as book view models</returns>
	Task<IEnumerable<BookViewModel>> All();

	/// <summary>
	/// Adds a book to the database
	/// </summary>
	/// <param name="viewModel">Book data</param>
	Task Add(BookViewModel viewModel);

	/// <summary>
	/// Adds a book to the user's read list
	/// </summary>
	/// <param name="id">Book Id</param>
	/// <param name="user">User</param>
	Task AddToReadList(Guid id, Guid userId);

	/// <summary>
	/// Removes a book from the user's read list
	/// </summary>
	/// <param name="id">Book Id</param>
	/// <param name="user">User</param>
	Task RemoveFromReadList(Guid id, Guid userId);

	/// <summary>
	/// Fetches book by id with included users
	/// </summary>
	/// <param name="bookId">Id of book</param>
	Task<Book?> GetByIdWithUsers(Guid bookId);

	/// <summary>
	/// Populates the ReadBy property of book view models
	/// </summary>
	/// <param name="books">Books</param>
	/// <param name="user">User to check for</param>
	void PopulateIsReadForViewModels(BookViewModel[] books, Guid user);
}

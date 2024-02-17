using BooksApp.Core.Contracts;
using BooksApp.Core.Models;
using BooksApp.Infrastructure.Data.Common;
using BooksApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Core.Services;

/// <summary>
/// Book service responsible for managing all the books business logic.
/// </summary>
public class BookService(
	IRepository repo
) : IBookService
{
	private readonly IRepository _repo = repo;

	/// <summary>
	/// Fetches all books from the database
	/// </summary>
	/// <returns>All books as book view models</returns>
	public async Task<IEnumerable<BookViewModel>> All()
	{
		var books = await this._repo
			.AllReadonly<Book>()
			.Include(x => x.Publisher)
			.Include(x => x.Users)
			.ToListAsync();

		var booksViewModels = books.Select(x => new BookViewModel()
		{
			Id = x.Id,
			ImageUrl = x.ImageUrl,
			Title = x.Title,
			Author = x.Author,
			ISBN = x.ISBN,
			Year = x.Year,
			PublisherId = x.PublisherId,
			PublisherName = x.Publisher.Name,
			ReadBy = x.Users.Select(x => x.Id).ToArray(),
		});

		return booksViewModels;
	}

	/// <summary>
	/// Fetches book by id with included users
	/// </summary>
	/// <param name="bookId">Id of book</param>
	public async Task<Book?> GetByIdWithUsers(Guid bookId)
	{
		Book? book = await this._repo.All<Book>()
			.Include(x => x.Users)
			.FirstOrDefaultAsync(x => x.Id == bookId);

		return book;
	}

	/// <summary>
	/// Adds a book to the database
	/// </summary>
	/// <param name="viewModel">Book data</param>
	public async Task Add(BookViewModel viewModel)
	{
		Book book = new()
		{
			Author = viewModel.Author,
			ImageUrl = viewModel.ImageUrl,
			ISBN = viewModel.ISBN,
			Title = viewModel.Title,
			Year = viewModel.Year,
			PublisherId = viewModel.PublisherId,
		};

		await this._repo.AddAsync(book);
		await this._repo.SaveChangesAsync();
	}

	/// <summary>
	/// Adds a book to the user's read list
	/// </summary>
	/// <param name="id">Book Id</param>
	/// <param name="user">User</param>
	public async Task AddToReadList(Guid bookId, Guid userId)
	{
		UserBook userBook = new UserBook()
		{
			BookId = bookId,
			UserId = userId
		};

		await this._repo.AddAsync(userBook);
		await this._repo.SaveChangesAsync();
	}

	/// <summary>
	/// Removes a book from the user's read list
	/// </summary>
	/// <param name="id">Book Id</param>
	/// <param name="user">User</param>
	public async Task RemoveFromReadList(Guid bookId, Guid userId)
	{
		UserBook? userBook = await this._repo.All<UserBook>()
			.FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == bookId);

		ArgumentNullException.ThrowIfNull(userBook, nameof(userBook));

		this._repo.Delete(userBook);
		await this._repo.SaveChangesAsync();
	}

	/// <summary>
	/// Populates the ReadBy property of book view models
	/// </summary>
	/// <param name="books">Books</param>
	/// <param name="user">User to check for</param>
	public void PopulateIsReadForViewModels(BookViewModel[] books, Guid userId)
	{
		for (int i = 0; i < books.Length; i++)
		{
			var book = books[i];
			book.IsRead = book.ReadBy.Contains(userId);
		}
	}
}

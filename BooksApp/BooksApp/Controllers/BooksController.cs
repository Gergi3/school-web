using BooksApp.Core.Contracts;
using BooksApp.Core.Models;
using BooksApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksApp.Controllers;

/// <summary>
/// Books controller responsible for managing all books.
/// </summary>
[Authorize]
public class BooksController(
	IBookService bookService,
	IPublisherService publisherService,
	IUserService userService
) : Controller
{
	public readonly IBookService _bookService = bookService;
	public readonly IPublisherService _publisherService = publisherService;
	public readonly IUserService _userService = userService;

	/// <summary>
	/// Displays all books
	/// </summary>
	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> Index([FromQuery] string? query)
	{
		BookViewModel[] booksViewModels = (await this._bookService.All()).ToArray();

		if (this.User?.Identity?.IsAuthenticated ?? false)
		{
			Guid currentUserId = this.GetCurrentUserId().ToGuid();
			this._bookService.PopulateIsReadForViewModels(booksViewModels, currentUserId);

			if (query != null && query == "read" || query == "unread")
			{
				bool isRead = query == "read";
				booksViewModels = booksViewModels.Where(x => x.IsRead == isRead).ToArray();
			}

			this.ViewBag.Checked = query;
		}

		return this.View(booksViewModels);
	}

	/// <summary>
	/// Displays a page for adding books
	/// </summary>
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Add()
	{
		var publishers = await this._publisherService.All();

		this.ViewBag.PublishersList = new SelectList(publishers, "Id", "Name");

		BookViewModel viewModel = new BookViewModel();
		return this.View(viewModel);
	}

	/// <summary>
	/// Handles the adding of a book
	/// </summary>
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Add(BookViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(viewModel);
		}

		await this._bookService.Add(viewModel);

		return this.RedirectToAction("index");
	}

	/// <summary>
	/// Adds book with sepecific id to read list of current user
	/// </summary>
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Read(
		[FromForm] string? query,
		[FromForm] Guid bookId)
	{
		Guid currentUserId = this.GetCurrentUserId().ToGuid();
		await this._bookService.AddToReadList(bookId, currentUserId);

		return this.RedirectToAction(nameof(Index), new { query });
	}

	/// <summary>
	/// Removes a book with sepecific id from the read list of current user
	/// </summary>
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Unread(
		[FromForm] string? query,
		[FromForm] Guid bookId)
	{
		Guid currentUserId = this.GetCurrentUserId().ToGuid();
		await this._bookService.RemoveFromReadList(bookId, currentUserId);

		return this.RedirectToAction(nameof(Index), new { query });
	}

	[NonAction]
	private Guid? GetCurrentUserId()
	{
		return this._userService.GetCurrentId(this.User)!;
	}
}

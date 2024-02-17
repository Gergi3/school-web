using BooksApp.Core.Contracts;
using BooksApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.Controllers;

[Authorize]
public class PublishersController(
	IPublisherService publisherService
) : Controller
{
	public readonly IPublisherService _publisherService = publisherService;

	/// <summary>
	/// Displays all books
	/// </summary>
	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> Index()
	{
		var publishersViewModel = await this._publisherService.All();

		return this.View(publishersViewModel);
	}

	/// <summary>
	/// Displays a page for adding books
	/// </summary>
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public IActionResult Add()
	{
		PublisherViewModel viewModel = new PublisherViewModel();
		return this.View(viewModel);
	}

	/// <summary>
	/// Handles the adding of a book
	/// </summary>
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Add(PublisherViewModel viewModel)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(viewModel);
		}
		await this._publisherService.Add(viewModel);

		return this.RedirectToAction("index");
	}
}

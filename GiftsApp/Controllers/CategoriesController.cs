using Microsoft.AspNetCore.Mvc;
using GiftsApp.Data;
using GiftsApp.Models.Entities;
using GiftsApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GiftsApp.Controllers;

[Authorize(Roles = "Administrator")]
public class CategoriesController : Controller
{
	private readonly ApplicationDbContext _context;

	public CategoriesController(ApplicationDbContext context)
	{
		this._context = context;
	}

	public IActionResult Create()
	{
		return this.View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
	{
		if (this.ModelState.IsValid)
		{
			Category category = new()
			{
				Id = Guid.NewGuid(),
				Name = categoryViewModel.Name,
			};

			await this._context.AddAsync(category);
			await this._context.SaveChangesAsync();

			return this.RedirectToAction("Index", "Home");
		}
		return this.View(categoryViewModel);
	}
}

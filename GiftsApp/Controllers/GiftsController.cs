using GiftsApp.Data;
using GiftsApp.Models.Entities;
using GiftsApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GiftsApp.Controllers;

[Authorize]
public class GiftsController : Controller
{
	private readonly ApplicationDbContext _context;

	public GiftsController(ApplicationDbContext context)
	{
		this._context = context;
	}

	// GET: Gifts
	[AllowAnonymous]
	public async Task<IActionResult> Index()
	{
		var gifts = await this._context.Gifts
			.Include(g => g.Category)
			.ToListAsync();

		var giftsViewModels = gifts.Select(x => new GiftViewModel()
		{
			MaxAge = x.MaxAge,
			MinAge = x.MinAge,
			Name = x.Name,
			CategoryName = x.Category.Name,
			CreatedAt = x.CreatedAt,
			Id = x.Id,
		});

		return this.View(giftsViewModels);
	}

	// GET: Gifts/Create
	[HttpGet]
	[Authorize(Roles = "SantaClaus,Administrator")]
	public IActionResult Create()
	{
		this.ViewData["CategoriesList"] = new SelectList(this._context.Categories, "Id", "Name");
		return this.View();
	}

	// POST: Gifts/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "SantaClaus,Administrator")]
	public async Task<IActionResult> Create(GiftSimpleViewModel giftViewModel)
	{
		if (this.ModelState.IsValid)
		{
			var gift = new Gift()
			{
				Id = Guid.NewGuid(),
				CategoryId = giftViewModel.CategoryId,
				MaxAge = giftViewModel.MaxAge,
				MinAge = giftViewModel.MinAge,
				Name = giftViewModel.Name
			};

			await this._context.AddAsync(gift);
			await this._context.SaveChangesAsync();
			return this.RedirectToAction(nameof(Index));
		}
		this.ViewData["CategoriesList"] = new SelectList(this._context.Categories, "Id", "Name", giftViewModel.CategoryId);
		return this.View(giftViewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize(Roles = "Administrator")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var gift = await this._context.Gifts.FindAsync(id);
		if (gift != null)
		{
			this._context.Gifts.Remove(gift);
		}

		await this._context.SaveChangesAsync();
		return this.RedirectToAction(nameof(Index));
	}
}

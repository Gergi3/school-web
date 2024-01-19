using GiftsApp.Data;
using GiftsApp.Models;
using GiftsApp.Models.Entities;
using GiftsApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GiftsApp.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly ApplicationDbContext _context;
	private readonly UserManager<User> _userManager;

	public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> userManager)
	{
		this._logger = logger;
		this._context = context;
		this._userManager = userManager;


	}

	public async Task<IActionResult> Index(string? takeGift)
	{
		string message = "";
		if (this.User.Identity != null
			&& this.User.Identity.IsAuthenticated
			&& takeGift != null
			&& takeGift.ToLower() == "yes")
		{
			if (!this._context.Gifts.Any())
			{
				message = "Няма налични подаръци за изтегляне, моля опитайте по-късно!";
			}
			else
			{
				int userAge = (await this._userManager.GetUserAsync(this.User))!.Age;
				Random rnd = new();

				var gifts = this._context.Gifts
					.Where(x => x.MaxAge >= userAge && x.MinAge <= userAge);

				if (!gifts.Any())
				{
					message = "Няма подходящи подаръци за изтегляне от вас, моля опитайте по-късно!";
				}
				else
				{
					int skipper = rnd.Next(0, gifts.Count());

					Gift gift = await gifts
						.OrderBy(p => Guid.NewGuid())
						.Skip(skipper)
						.Take(1)
						.FirstAsync();

					message = gift.Name;

					this._context.Gifts.Remove(gift);
					await this._context.SaveChangesAsync();
				}
			}
		}
		else if (takeGift != null
			&& takeGift.ToLower() == "yes")
		{
			message = "Трябва да сте влезли в профила си за да теглите подарък!";
		}

		GiftNameViewModel? giftViewModel = new GiftNameViewModel()
		{
			Name = message
		};

		return this.View(giftViewModel);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
	}
}

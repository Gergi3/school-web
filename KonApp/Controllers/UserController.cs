using KonApp.Models.Domain;
using KonApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KonApp.Controllers;

public class UserController : Controller

{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	public UserController(SignInManager<User> signInManager, UserManager<User> userManager)
	{
		this._userManager = userManager;
		this._signInManager = signInManager;
	}

	[HttpGet]
	public async Task<IActionResult> Login()
	{
		return this.View();
	}

	public async Task<IActionResult> Register()
	{
		return this.View();
	}

	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel registerData)
	{
		if (registerData == null)
		{
			return this.View(registerData);
		}

		User user = new User()
		{
			Email = registerData.Email,
			UserName = registerData.Email,
			FirstName = registerData.FirstName,
			LastName = registerData.LastName
		};

		var result = await this._userManager.CreateAsync(user, registerData.Password);

		if (result.Succeeded)
		{
			await this._signInManager.SignInAsync(user, isPersistent: false);
			return this.RedirectToAction("Home", "Index");
		}

		foreach (var item in result.Errors)
		{
			this.ModelState.AddModelError("", item.Description);
		}

		return this.View(registerData);
	}
}

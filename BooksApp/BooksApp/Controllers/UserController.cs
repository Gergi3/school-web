using BooksApp.Attributes;
using BooksApp.Core.Constants;
using BooksApp.Core.Contracts;
using BooksApp.Core.Models;
using BooksApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.Controllers;

/// <summary>
/// User controller responsible for managing all of the users.
/// Including all processes of authorization and authentication using Identity under the hood.
/// </summary>
/// <param name="_signInManager">Sign in manager</param>
/// <param name="_userService">User service</param>
public class UserController(
	SignInManager<User> _signInManager,
	IUserService _userService
	) : Controller
{

	/// <summary>
	/// Displays log in page.
	/// </summary>
	[HttpGet]
	[OnlyAnonymous]
	public IActionResult Login()
	{
		return this.View();
	}

	/// <summary>
	/// Processes login data provided by the user and acts accordingly,
	/// either logging in a user or redisplaying login with appropriate errors.
	/// </summary>
	/// <param name="userViewModel">Login view model containing user data</param>
	[HttpPost]
	[ValidateAntiForgeryToken]
	[OnlyAnonymous]
	public async Task<IActionResult> Login(LoginViewModel userViewModel)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(userViewModel);
		}

		User? user = await _userService.Get(userViewModel.Email);

		if (user == null)
		{
			this.ModelState.AddModelError("", ValidationMessages.UserNotFound);
			return this.View(userViewModel);
		}

		var result = await _signInManager.PasswordSignInAsync(
			user: user,
			password: userViewModel.Password,
			isPersistent: true,
			lockoutOnFailure: false
		);

		if (result.Succeeded)
		{
			return this.RedirectToAction("Index", "Books");
		}

		this.ModelState.AddModelError("", ValidationMessages.UserNotFound);
		return this.View(userViewModel);
	}

	/// <summary>
	/// Displays register page.
	/// </summary>
	[HttpGet]
	[OnlyAnonymous]
	public IActionResult Register()
	{
		return this.View();
	}

	/// <summary>
	/// Processes register data provided by the user and acts accordingly,
	/// either creating a user or redisplaying register with appropriate errors.
	/// </summary>
	/// <param name="userViewModel">Register view model containing user data</param>
	[HttpPost]
	[ValidateAntiForgeryToken]
	[OnlyAnonymous]
	public async Task<IActionResult> Register(RegisterViewModel userViewModel)
	{
		if (!this.ModelState.IsValid)
		{
			return this.View(userViewModel);
		}

		var (result, user) = await _userService.Create(userViewModel);
		if (result.Succeeded)
		{
			await _signInManager.SignInAsync(user, true);
			return this.RedirectToAction("Index", "Books");
		}

		foreach (IdentityError item in result.Errors)
		{
			this.ModelState.AddModelError("", item.Description);
		}

		return this.View(userViewModel);
	}


	/// <summary>
	/// Logs out user and redirects
	/// </summary>
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();

		return this.RedirectToAction("Index", "Books");
	}
}

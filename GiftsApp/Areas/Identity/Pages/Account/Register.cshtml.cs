// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using GiftsApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace GiftsApp.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
	private readonly SignInManager<User> _signInManager;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;
	private readonly IUserStore<User> _userStore;
	private readonly IUserEmailStore<User> _emailStore;
	private readonly ILogger<RegisterModel> _logger;
	private readonly IEmailSender _emailSender;

	public RegisterModel(
		UserManager<User> userManager,
		RoleManager<IdentityRole<Guid>> roleManager,
		IUserStore<User> userStore,
		SignInManager<User> signInManager,
		ILogger<RegisterModel> logger,
		IEmailSender emailSender)
	{

		this._userManager = userManager;
		this._roleManager = roleManager;
		this._userStore = userStore;
		this._emailStore = this.GetEmailStore();
		this._signInManager = signInManager;
		this._logger = logger;
		this._emailSender = emailSender;
	}

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	[BindProperty]
	public InputModel Input { get; set; }

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	public string ReturnUrl { get; set; }

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	public IList<AuthenticationScheme> ExternalLogins { get; set; }

	/// <summary>
	///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
	///     directly from your code. This API may change or be removed in future releases.
	/// </summary>
	public class InputModel
	{
		[Required]
		[Display(Name = "Първо име")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Фамилно име")]
		public string LastName { get; set; }

		[Required]
		[Display(Name = "Възраст")]
		public int Age { get; set; }
		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(Name = "Имейл")]
		public string Email { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessage = "{0}та трябва да е най-малко {2} символа и най-много {1} символа.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Парола")]
		public string Password { get; set; }

		/// <summary>
		///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
		///     directly from your code. This API may change or be removed in future releases.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "Повтори парола")]
		[Compare("Password", ErrorMessage = "Паролата трябва да съвпада с повторената парола.")]
		public string ConfirmPassword { get; set; }
	}

	public async Task OnGetAsync(string returnUrl = null)
	{
		this.ReturnUrl = returnUrl;
		this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
	}

	public async Task<IActionResult> OnPostAsync(string returnUrl = null)
	{
		returnUrl ??= this.Url.Content("~/");
		this.ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
		if (this.ModelState.IsValid)
		{
			var user = this.CreateUser();

			user.FirstName = this.Input.FirstName;
			user.LastName = this.Input.LastName;
			user.Age = this.Input.Age;
			await this._userStore.SetUserNameAsync(user, this.Input.Email, CancellationToken.None);
			await this._emailStore.SetEmailAsync(user, this.Input.Email, CancellationToken.None);
			var result = await this._userManager.CreateAsync(user, this.Input.Password);

			if (user.Email == "admin@gmail.com")
			{
				if (!await this._roleManager.RoleExistsAsync("Administrator"))
				{
					await this._roleManager.CreateAsync(new IdentityRole<Guid>("Administrator"));
				}
				await this._userManager.AddToRoleAsync(user, "Administrator");
			}
			else
			{
				if (!await this._roleManager.RoleExistsAsync("Child"))
				{
					await this._roleManager.CreateAsync(new IdentityRole<Guid>("Child"));
				}

				await this._userManager.AddToRoleAsync(user, "Child");
			}

			if (result.Succeeded)
			{
				this._logger.LogInformation("User created a new account with password.");

				var userId = await this._userManager.GetUserIdAsync(user);
				var code = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
				var callbackUrl = this.Url.Page(
					"/Account/ConfirmEmail",
					pageHandler: null,
					values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
					protocol: this.Request.Scheme);

				await this._emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
					$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

				if (this._userManager.Options.SignIn.RequireConfirmedAccount)
				{
					return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
				}
				else
				{
					await this._signInManager.SignInAsync(user, isPersistent: false);
					return this.LocalRedirect(returnUrl);
				}
			}
			foreach (var error in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		// If we got this far, something failed, redisplay form
		return this.Page();
	}

	private User CreateUser()
	{
		try
		{
			return Activator.CreateInstance<User>();
		}
		catch
		{
			throw new InvalidOperationException($"Can't create an instance of '{nameof(this.User)}'. " +
				$"Ensure that '{nameof(this.User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
				$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
		}
	}

	private IUserEmailStore<User> GetEmailStore()
	{
		if (!this._userManager.SupportsUserEmail)
		{
			throw new NotSupportedException("The default UI requires a user store with email support.");
		}
		return (IUserEmailStore<User>)this._userStore;
	}
}

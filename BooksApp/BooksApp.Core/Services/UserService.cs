using System.Security.Claims;
using BooksApp.Core.Contracts;
using BooksApp.Core.Models;
using BooksApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Core.Services;

/// <summary>
/// User service responsible for managing all the authentication and authorization processes.
/// </summary>
public class UserService : IUserService
{
	private readonly UserManager<User> _userManager;

	/// <summary>
	/// DI the user manager.
	/// </summary>
	public UserService(UserManager<User> userManager)
	{
		this._userManager = userManager;
	}

	/// <summary>
	/// Creates a user and adds it to the database.
	/// </summary>
	/// <param name="viewModel">Register view model transfering user data</param>
	public async Task<(IdentityResult, User)> Create(RegisterViewModel viewModel)
	{
		var user = new User()
		{
			UserName = viewModel.Email,
			Email = viewModel.Email,
			EmailConfirmed = true
		};

		var result = await this._userManager.CreateAsync(user, viewModel.Password);

		return (result, user);
	}

	/// <summary>
	/// Fetches a user by email.
	/// </summary>
	/// <param name="email">User email</param>
	public async Task<User?> Get(string email)
	{
		var user = await this._userManager
			.Users
			.FirstAsync(x => email == x.Email);

		return user;
	}

	/// <summary>
	/// Fetches a user by id.
	/// </summary>
	/// <param name="email">User email</param>
	public async Task<User?> Get(Guid id)
	{
		var user = await this._userManager
			.Users
			.FirstOrDefaultAsync(x => id == x.Id);

		return user;
	}

	/// <summary>
	/// Fetches the current user from the database
	/// </summary>
	/// <param name="currentUserPrincipal">User claim</param>
	/// <returns>Current user</returns>
	public async Task<User?> GetCurrent(ClaimsPrincipal currentUserPrincipal)
	{
		Guid? currentUserId = this.GetCurrentId(currentUserPrincipal);
		User? user = null;

		if (currentUserId != null)
		{
			user = await this._userManager.Users
				.FirstOrDefaultAsync(x => x.Id == currentUserId);
		}

		return user;
	}

	/// <summary>
	/// Fetches the current user's id from the database
	/// </summary>
	/// <param name="currentUserPrincipal">User claim</param>
	/// <returns>Current user id</returns>
	public Guid? GetCurrentId(ClaimsPrincipal currentUserPrincipal)
	{
		string? userIdString =
			currentUserPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

		if (userIdString == null)
		{
			return null;
		}

		return new Guid(userIdString);
	}

}

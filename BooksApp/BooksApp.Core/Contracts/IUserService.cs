using System.Security.Claims;
using BooksApp.Core.Models;
using BooksApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BooksApp.Core.Contracts;

/// <summary>
/// User service responsible for managing all the authentication and authorization processes.
/// </summary>
public interface IUserService
{
	/// <summary>
	/// Creates a user and adds it to the database.
	/// </summary>
	/// <param name="viewModel">Register view model transfering user data</param>
	Task<(IdentityResult, User)> Create(RegisterViewModel viewModel);

	/// <summary>
	/// Fetches a user from the database.
	/// </summary>
	/// <param name="email">User email</param>
	Task<User?> Get(string email);

	/// <summary>
	/// Fetches the current user from the database
	/// </summary>
	/// <param name="currentUserPrincipal">User claim</param>
	/// <returns>Current user</returns>
	Task<User?> GetCurrent(ClaimsPrincipal currentUserPrincipal);

	/// <summary>
	/// Fetches the current user id from the database
	/// </summary>
	/// <param name="currentUserPrincipal">User claim</param>
	/// <returns>Current user id</returns>
	Guid? GetCurrentId(ClaimsPrincipal currentUserPrincipal);
}

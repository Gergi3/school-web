
using GiftsApp.Data;
using GiftsApp.Models.Entities;
using GiftsApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftsApp.Controllers;

[Authorize(Roles = "Administrator")]
public class RoleController : Controller
{
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;
	private readonly UserManager<User> _userManager;
	private readonly ApplicationDbContext _context;
	public RoleController(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, ApplicationDbContext context)
	{
		this._roleManager = roleManager;
		this._userManager = userManager;
		this._context = context;
	}

	public async Task<IActionResult> Index()
	{
		var roles = await this._roleManager.Roles.ToListAsync();

		var rolesViewModels = roles.Select(x => new RoleViewModel()
		{
			Id = x.Id,
			Name = x.Name
		});
		return this.View(rolesViewModels);
	}

	[HttpPost]
	public async Task<IActionResult> Reset()
	{
		var transaction = await this._context.Database.BeginTransactionAsync();

		IList<IdentityRole<Guid>> roles = await this._roleManager.Roles.Where(x => x.Name != "Administrator").ToListAsync();

		foreach (var role in roles)
		{
			await this._roleManager.DeleteAsync(role);
		}

		await this._roleManager.CreateAsync(new IdentityRole<Guid>("SantaClaus"));
		await this._roleManager.CreateAsync(new IdentityRole<Guid>("Child"));

		var users = this._userManager.Users;
		foreach (var user in users)
		{
			if (!await this._userManager.IsInRoleAsync(user, "Administrator"))
			{
				await this._userManager.AddToRoleAsync(user, "Child");
			}
		}

		await transaction.CommitAsync();

		//foreach (var user in users)
		//{
		//	await this._userManager.RemoveFromRolesAsync(user, this._roleManager.);
		//}

		return this.RedirectToAction("Index");
	}

	public async Task<IActionResult> Manage(Guid id)
	{
		var role = await this._roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
		if (role == null)
		{
			return this.NotFound();
		}

		var users = await this._userManager.Users.ToListAsync();
		this.ViewBag.UsersList = users;

		ManageRoleViewModel manageRoleViewModel = new()
		{
			RoleId = role.Id,
			RoleName = role.Name,
			UserIds = new CheckboxViewModel[users.Count]
		};

		return this.View(manageRoleViewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Manage(ManageRoleViewModel manageRoleViewModel)
	{
		foreach (var item in manageRoleViewModel.UserIds)
		{
			var user = await this._userManager.Users.FirstOrDefaultAsync(x => x.Id == item.Id);
			if (user == null)
			{
				return this.View(manageRoleViewModel);
			}

			if (item.IsChecked)
			{
				await this._userManager.AddToRoleAsync(user, manageRoleViewModel.RoleName);
			}
			else
			{
				await this._userManager.RemoveFromRoleAsync(user, manageRoleViewModel.RoleName);
			}
		}

		return this.RedirectToAction("Index");
	}

	[HttpPost]
	public async Task<IActionResult> Delete(Guid id)
	{
		var role = await this._roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
		if (role == null)
		{
			return this.NotFound();
		}
		if (role.Name == "Administrator")
		{
			return this.RedirectToAction("Index");
		}

		await this._roleManager.DeleteAsync(role);

		return this.RedirectToAction("Index");
	}
}

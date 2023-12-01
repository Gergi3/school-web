using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KonApp.Data;
using KonApp.Models.Domain;
using KonApp.ViewModels;

namespace KonApp.Controllers;

public class BreedsController : Controller
{
	private readonly KonAppDbContext _context;

	public BreedsController(KonAppDbContext context)
	{
		this._context = context;
	}

	// GET: Breeds
	public async Task<IActionResult> Index()
	{
		List<Breed> breeds = await this._context.Breeds.ToListAsync();

		IEnumerable<BreedViewModel> breedsViewModel = breeds.Select(breed =>
			new BreedViewModel()
			{
				Id = breed.Id,
				Name = breed.Name
			}
		);

		return this.View(breedsViewModel);
	}

	// GET: Breeds/Details/5
	public async Task<IActionResult> Details(Guid? id)
	{
		if (id == null)
		{
			return this.NotFound();
		}

		Breed? breed = await this._context.Breeds
			.FirstOrDefaultAsync(m => m.Id == id);
		if (breed == null)
		{
			return this.NotFound();
		}

		BreedViewModel breedViewModel = new BreedViewModel()
		{
			Name = breed.Name,
			Id = breed.Id
		};

		return this.View(breedViewModel);
	}

	// GET: Breeds/Create
	public IActionResult Create()
	{
		return this.View();
	}

	// POST: Breeds/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("Id,Name")] BreedViewModel breedViewModel)
	{
		if (this.ModelState.IsValid)
		{
			Breed breed = new Breed()
			{
				Id = Guid.NewGuid(),
				Name = breedViewModel.Name
			};

			await this._context.Breeds.AddAsync(breed);
			await this._context.SaveChangesAsync();
			return this.RedirectToAction(nameof(Index));
		}
		return this.View(breedViewModel);
	}

	// GET: Breeds/Edit/5
	public async Task<IActionResult> Edit(Guid? id)
	{
		if (id == null)
		{
			return this.NotFound();
		}

		var breed = await this._context.Breeds.FindAsync(id);
		if (breed == null)
		{
			return this.NotFound();
		}

		BreedViewModel breedViewModel = new BreedViewModel()
		{
			Name = breed.Name,
			Id = breed.Id
		};

		return this.View(breedViewModel);
	}

	// POST: Breeds/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] BreedViewModel breedViewModel)
	{
		if (id != breedViewModel.Id)
		{
			return this.NotFound();
		}

		if (this.ModelState.IsValid)
		{
			try
			{
				Breed? breed = await this._context.Breeds.FindAsync(id);
				if (breed == null)
				{
					return this.NotFound();
				}

				breed.Name = breedViewModel.Name;
				await this._context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!this.BreedExists(breedViewModel.Id))
				{
					return this.NotFound();
				}
				else
				{
					throw;
				}
			}
			return this.RedirectToAction(nameof(Index));
		}
		return this.View(breedViewModel);
	}

	// GET: Breeds/Delete/5
	public async Task<IActionResult> Delete(Guid? id)
	{
		if (id == null)
		{
			return this.NotFound();
		}

		var breed = await this._context.Breeds
			.FirstOrDefaultAsync(m => m.Id == id);
		if (breed == null)
		{
			return this.NotFound();
		}

		BreedViewModel breedViewModel = new BreedViewModel()
		{
			Name = breed.Name,
			Id = breed.Id
		};

		return this.View(breedViewModel);
	}

	// POST: Breeds/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		var breed = await this._context.Breeds.FindAsync(id);
		if (breed != null)
		{
			this._context.Breeds.Remove(breed);
		}

		await this._context.SaveChangesAsync();
		return this.RedirectToAction(nameof(Index));
	}

	private bool BreedExists(Guid id)
	{
		return this._context.Breeds.Any(e => e.Id == id);
	}
}

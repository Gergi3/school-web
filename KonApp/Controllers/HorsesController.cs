using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KonApp.Data;
using KonApp.Models.Domain;
using KonApp.ViewModels;

namespace KonApp.Controllers;

public class HorsesController : Controller
{
	private readonly KonAppDbContext _context;

	public HorsesController(KonAppDbContext context)
	{
		this._context = context;
	}

	// GET: Horses
	public async Task<IActionResult> Index()
	{
		List<Horse> horses = await this._context.Horses.Include(h => h.Breed).ToListAsync();
		IEnumerable<HorseViewModel> horseViewModels = horses.Select(h => new HorseViewModel()
		{
			Age = h.Age,
			Id = h.Id,
			ImageUrl = h.ImageUrl,
			Name = h.Name,
			Breed = new()
			{
				Name = h.Breed.Name,
				Id = h.Breed.Id
			}
		}).ToHashSet();

		return this.View(horseViewModels);
	}

	// GET: Horses/Details/5
	public async Task<IActionResult> Details(Guid? id)
	{
		if (id == null)
		{
			return this.NotFound();
		}

		Horse? horse = await this._context.Horses
			.Include(h => h.Breed)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (horse == null)
		{
			return this.NotFound();
		}

		HorseViewModel horseViewModel = new()
		{
			Age = horse.Age,
			Breed = new()
			{
				Name = horse.Breed.Name,
				Id = horse.Breed.Id
			},
			Id = horse.Id,
			ImageUrl = horse.ImageUrl,
			Name = horse.Name
		};

		return this.View(horseViewModel);
	}

	// GET: Horses/Create
	public async Task<IActionResult> Create()
	{
		List<Breed> breeds = await this._context.Breeds.ToListAsync();
		IEnumerable<BreedViewModel> breedViewModels = breeds.Select(b => new BreedViewModel()
		{
			Name = b.Name,
			Id = b.Id
		}).ToHashSet();

		this.ViewData["BreedsViewModels"] = new SelectList(breedViewModels, "Id", "Name");
		return this.View();
	}

	// POST: Horses/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(HorseSimpleViewModel horseViewModel)
	{
		if (this.ModelState.IsValid)
		{
			Horse horse = new Horse()
			{
				Id = Guid.NewGuid(),
				Age = horseViewModel.Age,
				ImageUrl = horseViewModel.ImageUrl,
				Name = horseViewModel.Name,
				BreedId = horseViewModel.BreedId,
			};

			this._context.Add(horse);
			await this._context.SaveChangesAsync();
			return this.RedirectToAction(nameof(Index));
		}

		List<Breed> breeds = await this._context.Breeds.ToListAsync();
		IEnumerable<BreedViewModel> breedViewModels = breeds.Select(b => new BreedViewModel()
		{
			Name = b.Name,
			Id = b.Id
		}).ToHashSet();

		this.ViewData["BreedsViewModels"] = new SelectList(breedViewModels, "Id", "Name", horseViewModel.BreedId);
		return this.View(horseViewModel);
	}

	// GET: Horses/Edit/5
	public async Task<IActionResult> Edit(Guid? id)
	{
		if (id == null)
		{
			return this.NotFound();
		}

		Horse? horse = await this._context.Horses.FindAsync(id);
		if (horse == null)
		{
			return this.NotFound();
		}

		HorseSimpleViewModel horseViewModel = new()
		{
			Age = horse.Age,
			BreedId = horse.BreedId,
			Id = horse.Id,
			ImageUrl = horse.ImageUrl,
			Name = horse.Name
		};

		List<Breed> breeds = await this._context.Breeds.ToListAsync();
		IEnumerable<BreedViewModel> breedViewModels = breeds.Select(b => new BreedViewModel()
		{
			Name = b.Name,
			Id = b.Id
		}).ToHashSet();

		this.ViewData["BreedsViewModels"] = new SelectList(breedViewModels, "Id", "Name", horseViewModel.BreedId);
		return this.View(horseViewModel);
	}

	// POST: Horses/Edit/5
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(
		Guid id,
		[Bind("Id,Name,Age,ImageUrl,BreedId")] HorseSimpleViewModel horseViewModel
	)
	{
		if (id != horseViewModel.Id)
		{
			return this.NotFound();
		}

		if (this.ModelState.IsValid)
		{
			Horse? horse = await this._context.Horses.FindAsync(id);
			if (horse is null)
			{
				return this.NotFound();
			}

			horse.Name = horseViewModel.Name;
			horse.BreedId = horseViewModel.BreedId;
			horse.ImageUrl = horseViewModel.ImageUrl;
			horse.Age = horseViewModel.Age;

			await this._context.SaveChangesAsync();
			return this.RedirectToAction(nameof(Index));
		}

		List<Breed> breeds = await this._context.Breeds.ToListAsync();
		IEnumerable<BreedViewModel> breedViewModels = breeds.Select(b => new BreedViewModel()
		{
			Name = b.Name,
			Id = b.Id
		}).ToHashSet();

		this.ViewData["BreedsViewModels"] = new SelectList(breedViewModels, "Id", "Name", horseViewModel.BreedId);
		return this.View(horseViewModel);
	}

	// GET: Horses/Delete/5
	public async Task<IActionResult> Delete(Guid? id)
	{
		if (id == null)
		{
			return this.NotFound();
		}

		Horse? horse = await this._context.Horses
			.Include(h => h.Breed)
			.FirstOrDefaultAsync(m => m.Id == id);
		if (horse == null)
		{
			return this.NotFound();
		}

		Breed breed = horse.Breed;
		HorseViewModel horseViewModel = new()
		{
			Name = horse.Name,
			ImageUrl = horse.ImageUrl,
			Breed = new()
			{
				Name = breed.Name,
				Id = breed.Id
			},
			Age = horse.Age,
			Id = horse.Id
		};

		return this.View(horseViewModel);
	}

	// POST: Horses/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(Guid id)
	{
		var horse = await this._context.Horses.FindAsync(id);
		if (horse != null)
		{
			this._context.Horses.Remove(horse);
		}

		await this._context.SaveChangesAsync();
		return this.RedirectToAction(nameof(Index));
	}

	private bool HorseExists(Guid id)
	{
		return this._context.Horses.Any(e => e.Id == id);
	}
}

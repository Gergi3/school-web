using BooksApp.Core.Contracts;
using BooksApp.Core.Models;
using BooksApp.Infrastructure.Data.Common;
using BooksApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Core.Services;

/// <summary>
/// Publisher service responsible for managing all the publishers business logic.
/// </summary>
public class PublisherService(
	IRepository repo
) : IPublisherService
{
	private readonly IRepository _repo = repo;

	/// <summary>
	/// Fetches all publishers from the database
	/// </summary>
	/// <returns>All publishers as publisher view models</returns>
	public async Task<IEnumerable<PublisherViewModel>> All()
	{
		var publishers = await this._repo
			.AllReadonly<Publisher>()
			.ToListAsync();

		var publisherViewModels = publishers.Select(x => new PublisherViewModel()
		{
			Id = x.Id,
			Name = x.Name
		});

		return publisherViewModels;
	}

	/// <summary>
	/// Adds a publisher to the database
	/// </summary>
	/// <param name="viewModel">Publisher data</param>
	public async Task Add(PublisherViewModel viewModel)
	{
		Publisher publisher = new()
		{
			Name = viewModel.Name,
			Id = viewModel.Id,
		};

		await this._repo.AddAsync(publisher);
		await this._repo.SaveChangesAsync();
	}
}

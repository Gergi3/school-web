using BooksApp.Core.Contracts;
using BooksApp.Core.Services;
using BooksApp.Infrastructure.Data.Common;

namespace BooksApp.Extensions;

public static class BooksAppServiceCollectionExtension
{
	/// <summary>
	/// Adds services to the IoC container.
	/// </summary>
	/// <param name="services">Service Collection from the IoC container</param>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IRepository, Repository>();
		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IBookService, BookService>();
		services.AddScoped<IPublisherService, PublisherService>();

		return services;
	}
}

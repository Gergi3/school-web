using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BooksApp.Infrastructure.Data.Models;

namespace BooksApp.Infrastructure.Data.Configuration;
/// <summary>
/// Contains methods for configuring Publisher to have default values
/// </summary>
internal class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
	/// <summary>
	/// Adds default publishers to builder
	/// </summary>
	public void Configure(EntityTypeBuilder<Publisher> builder)
	{
		builder.HasData(CreatePublishers());
	}

	/// <summary>
	/// Creates the default publishers
	/// </summary>
	private static List<Publisher> CreatePublishers()
	{
		var publishers = new List<Publisher>();

		publishers.AddRange([
			new()
			{
				Id = new Guid("75bffea8-b008-422e-86a6-382652d4304c"),
				Name = "W. W. Norton & Company"
			},
			new()
			{
				Id = new Guid("edf03e24-c20e-4978-bdcb-a932133fe92b"),
				Name = "Arrow Books"
			},
			new()
			{
				Id = new Guid("37f379ea-fa20-4d29-adfb-3489f0737f9a"),
				Name = "Modern Library"
			},
		]);

		return publishers;
	}
}

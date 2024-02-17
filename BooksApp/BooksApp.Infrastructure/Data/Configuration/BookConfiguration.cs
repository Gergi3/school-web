using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BooksApp.Infrastructure.Data.Models;

namespace BooksApp.Infrastructure.Data.Configuration;
/// <summary>
/// Contains methods for configuring Book to have default values
/// </summary>
internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
	/// <summary>
	/// Adds default books to builder
	/// </summary>
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.HasData(CreateBooks());
	}

	/// <summary>
	/// Creates the default books
	/// </summary>
	private static List<Book> CreateBooks()
	{
		var books = new List<Book>();

		books.AddRange([
			new Book()
			{
				Id = new Guid("9d7dea10-2256-48a3-91d3-d3889311adc4"),
				Title = "Fight Club",
				Year = 1996,
				ISBN = "9780393355949",
				Author = "Chuck Palahniuk",
				ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1558216416i/36236124.jpg",
				PublisherId = new Guid("75bffea8-b008-422e-86a6-382652d4304c"),
			},
			new Book()
			{
				Id = new Guid("377c49ce-6061-4c02-ba6f-d3870267db95"),
				Title = "A Farewell to Arms",
				Year = 1929,
				ISBN = "9780099910107",
				Author = "Ernest Hemingway",
				ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1313714836i/10799.jpg",
				PublisherId = new Guid("edf03e24-c20e-4978-bdcb-a932133fe92b"),
			},
			new Book()
			{
				Id = new Guid("cbfa9db5-a7bf-423c-84a5-c0672ab816ac"),
				Title = "Personal Memoirs",
				Year = 1885,
				ISBN = "9780965393423",
				Author = "Ulysses S. Grant, Geoffrey Perrett",
				ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1561995270i/116933.jpg",
				PublisherId = new Guid("37f379ea-fa20-4d29-adfb-3489f0737f9a"),
			},
			new Book()
			{
				Id = new Guid("5eea1fbd-910d-4db7-9752-aa6881c87638"),
				Title = "The Picture of Dorian Gray",
				Year = 1890,
				ISBN = "9780965393136",
				Author = "Oscar Wilde, Jeffrey Eugenides ",
				ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1546103428i/5297.jpg",
				PublisherId = new Guid("37f379ea-fa20-4d29-adfb-3489f0737f9a"),
			},
		]);

		return books;
	}
}

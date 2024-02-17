using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BooksApp.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class seedbooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bc38e69-3b75-45b3-b3e5-7feaf9db4f05", "AQAAAAIAAYagAAAAEIaSW4Jtl494qMTe/ykC3aD1sgoOk1RzdT4pTg6eja6quICEGPh2HylmF5PZ5w5XHg==", "c4e90905-53e3-4967-b542-b2f72abf8993" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea12856-c198-4129-b3f3-b893d8395082"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f07545-fb1d-4da8-b751-e35d40b1b087", "AQAAAAIAAYagAAAAEFYAtTnrvnyF7OoIM4wKQrvPuVBlXk2fprkZjMQ/AY0WcWOAeOnalWJIrrAc0gH6Fg==", "6b314db3-5096-4e6c-be92-3e5a68fc7bb1" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "ISBN", "ImageUrl", "PublisherId", "Title", "Year" },
                values: new object[,]
                {
                    { new Guid("377c49ce-6061-4c02-ba6f-d3870267db95"), "Ernest Hemingway", "9780099910107", "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1313714836i/10799.jpg", new Guid("edf03e24-c20e-4978-bdcb-a932133fe92b"), "A Farewell to Arms", 1929 },
                    { new Guid("5eea1fbd-910d-4db7-9752-aa6881c87638"), "Oscar Wilde, Jeffrey Eugenides ", "9780965393136", "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1546103428i/5297.jpg", new Guid("37f379ea-fa20-4d29-adfb-3489f0737f9a"), "The Picture of Dorian Gray", 1890 },
                    { new Guid("9d7dea10-2256-48a3-91d3-d3889311adc4"), "Chuck Palahniuk", "9780393355949", "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1558216416i/36236124.jpg", new Guid("75bffea8-b008-422e-86a6-382652d4304c"), "Fight Club", 1996 },
                    { new Guid("cbfa9db5-a7bf-423c-84a5-c0672ab816ac"), "Ulysses S. Grant, Geoffrey Perrett", "9780965393423", "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1561995270i/116933.jpg", new Guid("37f379ea-fa20-4d29-adfb-3489f0737f9a"), "Personal Memoirs", 1885 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("377c49ce-6061-4c02-ba6f-d3870267db95"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5eea1fbd-910d-4db7-9752-aa6881c87638"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("9d7dea10-2256-48a3-91d3-d3889311adc4"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("cbfa9db5-a7bf-423c-84a5-c0672ab816ac"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad1ed4d6-8b5c-4fa6-a2a5-9cf307b8a1d1", "AQAAAAIAAYagAAAAEDo4cl7xGxVdDgaHoQ2bTzblYttFXzTKkOxWRinbgJ//0/FX2zSOy+IjYaFuLqQgxA==", "a968e06b-8ed9-47fa-8f96-6c4224a37a33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea12856-c198-4129-b3f3-b893d8395082"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23f160e6-eb0e-455d-9e86-891b152275c7", "AQAAAAIAAYagAAAAEP4MMourau2ArHEfSKpmEczpP5mYb3WY/LksZHt7yUyH1iBT7omRrwPRW60ZZF81JQ==", "86c3124f-e374-4d66-b92c-444f5bdb34a0" });
        }
    }
}

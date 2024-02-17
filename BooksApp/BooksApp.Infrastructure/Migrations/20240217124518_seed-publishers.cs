using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BooksApp.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class seedpublishers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("37f379ea-fa20-4d29-adfb-3489f0737f9a"), "Modern Library" },
                    { new Guid("75bffea8-b008-422e-86a6-382652d4304c"), "W. W. Norton & Company" },
                    { new Guid("edf03e24-c20e-4978-bdcb-a932133fe92b"), "Arrow Books" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("37f379ea-fa20-4d29-adfb-3489f0737f9a"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("75bffea8-b008-422e-86a6-382652d4304c"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("edf03e24-c20e-4978-bdcb-a932133fe92b"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9fd270e-8c62-4b28-936a-ebb522362f10", "AQAAAAIAAYagAAAAELUyJMVn0UXpzhd2qf5td+YRsCDdxL5pU0DGW51xnZKduKC+ngp21uw+eqe3UnnBVg==", "526a71d1-27a9-475c-9f88-426d450f779a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dea12856-c198-4129-b3f3-b893d8395082"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a64d52fc-5ac1-4dfe-af29-7e0f542caa79", "AQAAAAIAAYagAAAAEDac/1/vkTAjY17/ysbv+G9LiHBapAsu8cpB+Q6YZoYDaCh7tFOqeRXYOIhIw1Aadw==", "db3c710b-0705-4165-baeb-41a3e74fdd61" });
        }
    }
}

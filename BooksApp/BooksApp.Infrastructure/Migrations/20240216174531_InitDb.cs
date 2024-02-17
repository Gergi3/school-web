using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BooksApp.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitDb : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "AspNetRoles",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoles", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUsers",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
				PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
				SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
				TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
				LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
				LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
				AccessFailedCount = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUsers", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Publishers",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identificator of the publisher"),
				Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Name of the publisher")
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Publishers", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetRoleClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetUserClaims_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserLogins",
			columns: table => new
			{
				LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
				table.ForeignKey(
					name: "FK_AspNetUserLogins_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserRoles",
			columns: table => new
			{
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserTokens",
			columns: table => new
			{
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
				Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
				table.ForeignKey(
					name: "FK_AspNetUserTokens_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "Books",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identificator of the book"),
				Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Title of the book"),
				Year = table.Column<int>(type: "int", nullable: false, comment: "Year in which the book was published"),
				Author = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false, comment: "Author by which the book was published"),
				ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "String representation of the barcode of the book"),
				ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Image URL of the cover of the book"),
				PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Published ID by which the book was published")
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Books", x => x.Id);
				table.ForeignKey(
					name: "FK_Books_Publishers_PublisherId",
					column: x => x.PublisherId,
					principalTable: "Publishers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "UserBook",
			columns: table => new
			{
				BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_UserBook", x => new { x.UserId, x.BookId });
				table.ForeignKey(
					name: "FK_UserBook_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_UserBook_Books_BookId",
					column: x => x.BookId,
					principalTable: "Books",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.InsertData(
			table: "AspNetRoles",
			columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
			values: new object[,]
			{
				{ new Guid("8c493038-059d-45d2-819e-ade531947a2b"), null, "Admin", null },
				{ new Guid("a470559d-3b1d-40de-9215-114eceb07393"), null, "User", null }
			});

		migrationBuilder.InsertData(
			table: "AspNetUsers",
			columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
			values: new object[,]
			{
				{ new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), 0, "a9fd270e-8c62-4b28-936a-ebb522362f10", "user@mail.com", false, false, null, "user@mail.com", "user@mail.com", "AQAAAAIAAYagAAAAELUyJMVn0UXpzhd2qf5td+YRsCDdxL5pU0DGW51xnZKduKC+ngp21uw+eqe3UnnBVg==", null, false, "526a71d1-27a9-475c-9f88-426d450f779a", false, "user@mail.com" },
				{ new Guid("dea12856-c198-4129-b3f3-b893d8395082"), 0, "a64d52fc-5ac1-4dfe-af29-7e0f542caa79", "admin@mail.com", false, false, null, "admin@mail.com", "admin@mail.com", "AQAAAAIAAYagAAAAEDac/1/vkTAjY17/ysbv+G9LiHBapAsu8cpB+Q6YZoYDaCh7tFOqeRXYOIhIw1Aadw==", null, false, "db3c710b-0705-4165-baeb-41a3e74fdd61", false, "admin@mail.com" }
			});

		migrationBuilder.InsertData(
			table: "AspNetUserRoles",
			columns: new[] { "RoleId", "UserId" },
			values: new object[,]
			{
				{ new Guid("a470559d-3b1d-40de-9215-114eceb07393"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e") },
				{ new Guid("8c493038-059d-45d2-819e-ade531947a2b"), new Guid("dea12856-c198-4129-b3f3-b893d8395082") }
			});

		migrationBuilder.CreateIndex(
			name: "IX_AspNetRoleClaims_RoleId",
			table: "AspNetRoleClaims",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "RoleNameIndex",
			table: "AspNetRoles",
			column: "NormalizedName",
			unique: true,
			filter: "[NormalizedName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserClaims_UserId",
			table: "AspNetUserClaims",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserLogins_UserId",
			table: "AspNetUserLogins",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserRoles_RoleId",
			table: "AspNetUserRoles",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "EmailIndex",
			table: "AspNetUsers",
			column: "NormalizedEmail");

		migrationBuilder.CreateIndex(
			name: "UserNameIndex",
			table: "AspNetUsers",
			column: "NormalizedUserName",
			unique: true,
			filter: "[NormalizedUserName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_Books_PublisherId",
			table: "Books",
			column: "PublisherId");

		migrationBuilder.CreateIndex(
			name: "IX_UserBook_BookId",
			table: "UserBook",
			column: "BookId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "AspNetRoleClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserLogins");

		migrationBuilder.DropTable(
			name: "AspNetUserRoles");

		migrationBuilder.DropTable(
			name: "AspNetUserTokens");

		migrationBuilder.DropTable(
			name: "UserBook");

		migrationBuilder.DropTable(
			name: "AspNetRoles");

		migrationBuilder.DropTable(
			name: "AspNetUsers");

		migrationBuilder.DropTable(
			name: "Books");

		migrationBuilder.DropTable(
			name: "Publishers");
	}
}

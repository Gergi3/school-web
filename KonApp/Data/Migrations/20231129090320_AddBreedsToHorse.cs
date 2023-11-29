using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBreedsToHorse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BreedId",
                table: "Horses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Horses_BreedId",
                table: "Horses",
                column: "BreedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horses_Breeds_BreedId",
                table: "Horses",
                column: "BreedId",
                principalTable: "Breeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horses_Breeds_BreedId",
                table: "Horses");

            migrationBuilder.DropIndex(
                name: "IX_Horses_BreedId",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "BreedId",
                table: "Horses");
        }
    }
}

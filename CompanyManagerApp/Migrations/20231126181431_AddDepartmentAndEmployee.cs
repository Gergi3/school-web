using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManagerApp.Migrations;

public partial class AddDepartmentAndEmployee : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Departments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Departments", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Employees",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Salary = table.Column<decimal>(type: "money", nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Employees", x => x.Id);
                table.ForeignKey(
                    name: "FK_Employees_Departments_DepartmentId",
                    column: x => x.DepartmentId,
                    principalTable: "Departments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Employees_DepartmentId",
            table: "Employees",
            column: "DepartmentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Employees");

        migrationBuilder.DropTable(
            name: "Departments");
    }
}

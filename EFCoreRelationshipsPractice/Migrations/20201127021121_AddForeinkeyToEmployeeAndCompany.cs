using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreRelationshipsPractice.Migrations
{
    public partial class AddForeinkeyToEmployeeAndCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyEntityID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CompanyEntityID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CompanyEntityID",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyID",
                table: "Employees",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyID",
                table: "Employees",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CompanyID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "CompanyEntityID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyEntityID",
                table: "Employees",
                column: "CompanyEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyEntityID",
                table: "Employees",
                column: "CompanyEntityID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreRelationshipsPractice.Migrations
{
    public partial class MoveProfileAndEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEntity_Companies_CompanyEntityID",
                table: "EmployeeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileEntity",
                table: "ProfileEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeEntity",
                table: "EmployeeEntity");

            migrationBuilder.RenameTable(
                name: "ProfileEntity",
                newName: "Profiles");

            migrationBuilder.RenameTable(
                name: "EmployeeEntity",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEntity_CompanyEntityID",
                table: "Employees",
                newName: "IX_Employees_CompanyEntityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Profiles_ProfileID",
                table: "Companies",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyEntityID",
                table: "Employees",
                column: "CompanyEntityID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Profiles_ProfileID",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyEntityID",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "ProfileEntity");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "EmployeeEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyEntityID",
                table: "EmployeeEntity",
                newName: "IX_EmployeeEntity_CompanyEntityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileEntity",
                table: "ProfileEntity",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeEntity",
                table: "EmployeeEntity",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileID",
                table: "Companies",
                column: "ProfileID",
                principalTable: "ProfileEntity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEntity_Companies_CompanyEntityID",
                table: "EmployeeEntity",
                column: "CompanyEntityID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

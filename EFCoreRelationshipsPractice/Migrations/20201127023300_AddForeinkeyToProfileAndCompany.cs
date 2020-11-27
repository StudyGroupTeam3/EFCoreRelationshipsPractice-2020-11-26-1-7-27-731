using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreRelationshipsPractice.Migrations
{
    public partial class AddForeinkeyToProfileAndCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Profiles_ProfileID",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProfileID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProfileID",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CompanyID",
                table: "Profiles",
                column: "CompanyID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Companies_CompanyID",
                table: "Profiles",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Companies_CompanyID",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_CompanyID",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "ProfileID",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProfileID",
                table: "Companies",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Profiles_ProfileID",
                table: "Companies",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

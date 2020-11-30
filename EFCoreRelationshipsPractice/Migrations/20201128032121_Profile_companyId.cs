using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreRelationshipsPractice.Migrations
{
    public partial class Profile_companyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Profiles_ProfileEntityId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProfileEntityId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProfileEntityId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CompanyId",
                table: "Profiles",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Companies_CompanyId",
                table: "Profiles",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Companies_CompanyId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_CompanyId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "ProfileEntityId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProfileEntityId",
                table: "Companies",
                column: "ProfileEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Profiles_ProfileEntityId",
                table: "Companies",
                column: "ProfileEntityId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

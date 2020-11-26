using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreRelationshipsPractice.Migrations
{
    public partial class AddProfileToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileID",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfileEntity",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegisteredCapital = table.Column<int>(nullable: false),
                    CertId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileEntity", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProfileID",
                table: "Companies",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileID",
                table: "Companies",
                column: "ProfileID",
                principalTable: "ProfileEntity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileID",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "ProfileEntity");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProfileID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProfileID",
                table: "Companies");
        }
    }
}

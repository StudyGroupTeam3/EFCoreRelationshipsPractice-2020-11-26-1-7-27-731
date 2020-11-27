using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreRelationshipsPractice.Migrations
{
    public partial class AddEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileEntitiesId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProfileEntitiesId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProfileEntitiesId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "ProfileEntityId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    CompanyEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEntity_Companies_CompanyEntityId",
                        column: x => x.CompanyEntityId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProfileEntityId",
                table: "Companies",
                column: "ProfileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEntity_CompanyEntityId",
                table: "EmployeeEntity",
                column: "CompanyEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileEntityId",
                table: "Companies",
                column: "ProfileEntityId",
                principalTable: "ProfileEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileEntityId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "EmployeeEntity");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProfileEntityId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProfileEntityId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "ProfileEntitiesId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProfileEntitiesId",
                table: "Companies",
                column: "ProfileEntitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ProfileEntity_ProfileEntitiesId",
                table: "Companies",
                column: "ProfileEntitiesId",
                principalTable: "ProfileEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kariyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationUpdate_Company_Industry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Industry_IndustryId",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_IndustryId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Company");

            migrationBuilder.CreateTable(
                name: "CompanyIndustry",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "integer", nullable: false),
                    IndustriesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyIndustry", x => new { x.CompaniesId, x.IndustriesId });
                    table.ForeignKey(
                        name: "FK_CompanyIndustry_Company_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyIndustry_Industry_IndustriesId",
                        column: x => x.IndustriesId,
                        principalTable: "Industry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyIndustry_IndustriesId",
                table: "CompanyIndustry",
                column: "IndustriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyIndustry");

            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "Company",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_IndustryId",
                table: "Company",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Industry_IndustryId",
                table: "Company",
                column: "IndustryId",
                principalTable: "Industry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kariyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingProperty_Company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndustaryId",
                table: "Company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IndustaryId",
                table: "Company",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

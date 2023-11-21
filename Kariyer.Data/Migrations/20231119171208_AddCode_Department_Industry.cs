using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kariyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCode_Department_Industry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Industry",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Department",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Industry");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Department");
        }
    }
}

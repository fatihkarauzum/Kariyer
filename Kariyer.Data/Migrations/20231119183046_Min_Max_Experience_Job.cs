using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kariyer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Min_Max_Experience_Job : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Job",
                newName: "MinExperience");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingMode",
                table: "Job",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "ExperienceType",
                table: "Job",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<int>(
                name: "MaxExperience",
                table: "Job",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EstablishmentYear",
                table: "Company",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxExperience",
                table: "Job");

            migrationBuilder.RenameColumn(
                name: "MinExperience",
                table: "Job",
                newName: "Experience");

            migrationBuilder.AlterColumn<byte>(
                name: "WorkingMode",
                table: "Job",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<byte>(
                name: "ExperienceType",
                table: "Job",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "EstablishmentYear",
                table: "Company",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class AddColumnCodeInLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Languages",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Languages");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

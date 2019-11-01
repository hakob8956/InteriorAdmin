using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class AddAllowNullCodeLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Languages",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Languages",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

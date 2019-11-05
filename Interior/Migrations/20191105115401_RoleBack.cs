using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class RoleBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Categories",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileId",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

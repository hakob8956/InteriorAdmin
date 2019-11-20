using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InteriorId",
                table: "Recommendations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InteriorId",
                table: "Recommendations",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

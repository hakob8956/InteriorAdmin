using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class AllowNullinteriorIdInContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InteriorId",
                table: "ContentAttachments",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InteriorId",
                table: "ContentAttachments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

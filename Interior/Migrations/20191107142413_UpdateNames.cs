using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class UpdateNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageId",
                table: "Interiors",
                newName: "IosFileId");

            migrationBuilder.RenameColumn(
                name: "fileIosId",
                table: "Interiors",
                newName: "ImageFileId");

            migrationBuilder.RenameColumn(
                name: "fileAndroidId",
                table: "Interiors",
                newName: "AndroidFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IosFileId",
                table: "Interiors",
                newName: "imageId");

            migrationBuilder.RenameColumn(
                name: "ImageFileId",
                table: "Interiors",
                newName: "fileIosId");

            migrationBuilder.RenameColumn(
                name: "AndroidFileId",
                table: "Interiors",
                newName: "fileAndroidId");
        }
    }
}
